using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using Spectre.Console;

namespace SemanticKernel.ChatHistoryApp
{
    public class ChatHistoryApp
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
        public ChatHistoryApp(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAIChatInitialize(OpenAIPromptExecutionSettings settings, ChatHistory history)
        {

            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAIChatCompletion(_model, _key)
               .Build();

            var chatCompletionService = openAIKernel.GetRequiredService<IChatCompletionService>();

            var response = await chatCompletionService.GetChatMessageContentAsync(history, settings); //Make sure to select the correct GetChatMessageContentAsync
            Console.WriteLine(response);


        }

        public void ShowWelcomeMessages()
        {
            AnsiConsole.MarkupLine("[bold green]Welcome to Semantic Kernel Chat Demo[/]");

            AnsiConsole.MarkupLine("Options");

            AnsiConsole.MarkupLine("- Type text and press Enter");

            AnsiConsole.MarkupLine("- Type [blue]img[/] to attach an image (local path or URL)");

            AnsiConsole.MarkupLine("- Type [red]exit[/] to terminate.\n");
        }

        public async Task RunChatLoopAsync(ChatHistory history, OpenAIPromptExecutionSettings settings)
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAIChatCompletion(_model, _key)
               .Build();

            var chatCompletionService = openAIKernel.GetRequiredService<IChatCompletionService>();

            while (true)
            {
                var userInput = AnsiConsole.Ask<string>("[blue]User:[/]");
                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    AnsiConsole.MarkupLine("[bold red]Exiting chat...[/]");
                    break;
                }
                else if (userInput.Equals("img", StringComparison.OrdinalIgnoreCase))
                {
                    var imagePathOrUrl = AnsiConsole.Prompt(
                    new TextPrompt<string>("[blue]Enter the image path (local) or URL:[/]")
                        .Validate(pathOrUrl =>
                        {
                            if (string.IsNullOrWhiteSpace(pathOrUrl))
                            {
                                return ValidationResult.Error("[red]The image path or URL cannot be empty.[/]");
                            }
                            return ValidationResult.Success();
                        })
                );

                    AnsiConsole.MarkupLine($"Do you want to add additional descriptive text?");
                    var additionalText =
                            AnsiConsole.Ask<string>("[blue](Leave empty if you don't want to add text):[/]");

                    var userMessageContents = await CreateUserContentAsync(additionalText, imagePathOrUrl);
                    if (userMessageContents is null)
                    {
                        continue;
                    }

                    history.AddUserMessage(userMessageContents);
                }
                else
                {
                    history.AddUserMessage(userInput);
                }

                try
                {
                    await AnsiConsole.Status()
               .Spinner(Spinner.Known.Balloon)
               .StartAsync("Thinking...", async ctx =>
               {
                   var response = await chatCompletionService.GetChatMessageContentAsync(history,
                       settings);
                   history.AddAssistantMessage(response.ToString());
               });

                    AnsiConsole.MarkupLine($"[bold green]Assistant:[/] {history.LastOrDefault()}\n");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]Error[/] {ex.Message}");
                }
            }
        }

        private async Task<ChatMessageContentItemCollection> CreateUserContentAsync(string additionalText, string imagePathOrUrl)
        {
            var contents = new ChatMessageContentItemCollection();

            if (!string.IsNullOrWhiteSpace(additionalText))
            {
                contents.Add(new TextContent(additionalText));
            }
            else
            {
                contents.Add(new TextContent("Give me the description of the image"));
            }


            if (imagePathOrUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                imagePathOrUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                contents.Add(new ImageContent(new Uri(imagePathOrUrl)));
            }
            else
            {
                AnsiConsole.MarkupLine($"[grey]Reading image from local path...[/]");
                if (!File.Exists(imagePathOrUrl))
                {
                    AnsiConsole.MarkupLine($"[red]The image path '{imagePathOrUrl}' does not exist.[/]");
                    return null;
                }
                try
                {
                    var imageBytes = File.ReadAllBytes(imagePathOrUrl);
                    var mimeType = InferMimeType(imagePathOrUrl);
                    contents.Add(new ImageContent(imageBytes, mimeType));
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error reading the image: {ex.Message}[/]");
                    return null;
                }

            }

            return contents;
        }

        private string InferMimeType(string localPath)
        {
            var extension = Path.GetExtension(localPath).ToLowerInvariant();
            return extension switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => "image/jpeg" // Default to jpeg if unknown
            };
        }
    }
}
