using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.SKKernel;

var openAIKey = Environment.GetEnvironmentVariable("SemanticKernelOpenAIKey", EnvironmentVariableTarget.User);

var azureRegion = Environment.GetEnvironmentVariable("SemanticKernelAzureRegion", EnvironmentVariableTarget.User);

var azureKey = Environment.GetEnvironmentVariable("SemanticKernelAzureKey", EnvironmentVariableTarget.User);

var modelIdOpenAI = "gpt-4o-mini-2024-07-18";

var options = new OpenAIPromptExecutionSettings // Set as many properties you want
{
    MaxTokens = 50,                             // Set the maximum number of tokens to return
    Temperature = 0.7f                          // Set the temperature for response variability
};

var prompt = "What is Semantic Kernel, describe briefly"; // What whaterver you want LLM to return


SKChat openAIChat = new SKChat(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await openAIChat.OpenAIChat(options);
//await openAIChat.AzureAIChat(options);
await openAIChat.OpenAIChatStreaming(options);
//await openAIChat.AzureAIChatStreaming(options);



