using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace SemanticKernel.PluginsDemo
{
    public class FunctionDemo
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
        public FunctionDemo(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAIChatInitialize(OpenAIPromptExecutionSettings settings)
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAIChatCompletion(_model, _key)
               .Build();

            KernelFunction timeFunction = KernelFunctionFactory.CreateFromMethod(() =>
            DateTime.Now.ToShortTimeString(),
            "get_current_time",
            "Gets the current time"
            );

            KernelFunction poemFunction = KernelFunctionFactory.CreateFromPrompt(
                "Write a poem about Semantic",
                functionName: "wirte_poem",
                description: "Writes a poem about Semantic Kernel"
                );

            var currentTime = await openAIKernel.InvokeAsync(timeFunction);
            var poem = await openAIKernel.InvokeAsync(poemFunction);
            Console.WriteLine($"Current Time: {currentTime}");
            Console.WriteLine($"Poem: {poem}");
        }

        public async Task AzureOpenAIChatInitialize(OpenAIPromptExecutionSettings settings, ChatHistory history)
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddAzureOpenAIChatCompletion(_model, endpoint: _region, apiKey: _key)
               .Build();
        }
    }
}
