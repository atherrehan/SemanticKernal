using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SemanticKernel.SKKernel
{
    public class SKChat
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
        public SKChat(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAIChat(OpenAIPromptExecutionSettings options)
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(_model, _key)
                .Build();

            var result = await openAIKernel.InvokePromptAsync(_prompt, new KernelArguments(options));

            Console.WriteLine(result);
        }

        public async Task OpenAIChatStreaming()
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(_model, _key)
                .Build();

            string fullMessage = string.Empty;
            await foreach (var result in openAIKernel.InvokePromptStreamingAsync<StreamingChatMessageContent>(_prompt))
            {
                if (result.Content is { Length: > 0 })
                {
                    fullMessage += result.Content;
                    Console.Write(result.Content);
                }
            }
        }

        public async Task AzureAIChat()
        {
            Kernel azureKernel = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(_model, _region, _key)
                .Build();

            var result = await azureKernel.InvokePromptAsync(_prompt);

            Console.WriteLine(result);

        }
       
        public async Task AzureAIChatStreaming()
        {
            Kernel azureKernel = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(_model, _region, _key)
                .Build();

            string fullMessage = string.Empty;
            await foreach (var result in azureKernel.InvokePromptStreamingAsync<StreamingChatMessageContent>(_prompt))
            {
                if (result.Content is { Length: > 0 })
                {
                    fullMessage += result.Content;
                    Console.Write(result.Content);
                }
            }

        }
    }
}
