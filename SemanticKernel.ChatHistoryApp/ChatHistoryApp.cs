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
    }
}
