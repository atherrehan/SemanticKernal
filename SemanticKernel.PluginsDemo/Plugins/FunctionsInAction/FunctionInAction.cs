using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using SemanticKernel.PluginsDemo.Plugins.Functions;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernel.PluginsDemo.Plugins.FunctionsInAction
{
    public class FunctionInAction
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
        public FunctionInAction(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }
        public async Task OpenAIFunctionInAction(OpenAIPromptExecutionSettings settings)
        {

            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAIChatCompletion(_model, _key)
               .Build();
            FunctionsPlugins plugins = new();
            openAIKernel.Plugins.AddFromFunctions("MyPlugin", [plugins.timeFunction, plugins.poemFunction]);
            var chatCompletionService = openAIKernel.GetRequiredService<IChatCompletionService>();
            OpenAIPromptExecutionSettings openAIPromptExecutionSettins = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
            };

            ChatHistory chatHistory = [];
            string? input = null;
            while (true)
            {
                Console.Write("\nUser >");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }
                chatHistory.AddUserMessage(input);
                var chatResult = await chatCompletionService.GetChatMessageContentAsync(
                    chatHistory,
                    openAIPromptExecutionSettins,
                    openAIKernel
                );
                chatHistory.AddAssistantMessage(chatResult.ToString());
                Console.WriteLine($"Assistanct >{chatResult}");
            }

        }
    }
}
