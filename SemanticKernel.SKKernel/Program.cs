using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.SKKernel;

#region Declaration

var openAIKey = Environment.GetEnvironmentVariable("SemanticKernelOpenAIKey", EnvironmentVariableTarget.User);

var azureRegion = Environment.GetEnvironmentVariable("SemanticKernelAzureRegion", EnvironmentVariableTarget.User);

var azureKey = Environment.GetEnvironmentVariable("SemanticKernelAzureKey", EnvironmentVariableTarget.User);

var modelIdOpenAI = "gpt-4o-mini-2024-07-18";

var options = new OpenAIPromptExecutionSettings // Set as many properties you want
{
    MaxTokens = 50,                             // Set the maximum number of tokens to return
    Temperature = 0.7f                          // Set the temperature for response variability
};

var prompt = string.Empty; // What whaterver you want LLM to return

#endregion

#region Charts

//prompt = "What is Semantic Kernel, describe briefly";
//SKChat chat = new SKChat(modelIdOpenAI, openAIKey ?? "", prompt, "");
////await chat.OpenAIChat(options);
////await chat.OpenAIChatStreaming(options);

//SKChat chat = new SKChat(modelIdOpenAI, openAIKey ?? "", prompt, azureRegion);
////await chat.AzureAIChat(options);
////await chat.AzureAIChatStreaming(options);
///
#endregion

#region Images

//prompt = "A futuristic city skyline at sunset with flying cars.";
//SKImage image = new SKImage(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await image.OpenAIImage();

//modelIdOpenAI = "dall-e-3";//Deploy base model in Azure
//SKImage image = new SKImage(modelIdOpenAI, openAIKey ?? "", prompt, azureRegion ?? "");
//await image.AzureAIImage();

#endregion