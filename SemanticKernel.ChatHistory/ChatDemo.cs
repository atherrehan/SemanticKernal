using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SemanticKernel.ChatHistoryDemo;

public class ChatDemo
{
    private readonly string _model;
    private readonly string _key;
    private readonly string _prompt;
    private readonly string _region;
    public ChatDemo(string model, string key, string prompt, string region)
    {
        _key = key;
        _model = model;
        _prompt = prompt;
        _region = region;
    }

    public async Task OpenAIChatCompletion(OpenAIPromptExecutionSettings settings, ChatHistory history = null)
    {

        Kernel openAIKernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(_model, _key)
           .Build();

        var chatCompletionService = openAIKernel.GetRequiredService<IChatCompletionService>();
        if (history is null)
        {
            var response = await chatCompletionService.GetChatMessageContentAsync(_prompt, settings); //Make sure to select the correct GetChatMessageContentAsync
            Console.WriteLine(response);
        }
        else
        {
            var response = await chatCompletionService.GetChatMessageContentAsync(history, settings); //Make sure to select the correct GetChatMessageContentAsync
            Console.WriteLine(response);
        }

    }

    public async Task AzureAIChatCompletion()
    {
        Kernel openAIKernel = Kernel.CreateBuilder()
          .AddAzureOpenAIChatCompletion(_model, endpoint: _region, apiKey: _key)
          .Build();
    }
}
