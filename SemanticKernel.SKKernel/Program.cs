using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var openAIKey = Environment.GetEnvironmentVariable("SemanticKernelOpenAIKey", EnvironmentVariableTarget.User);
var azureRegion = Environment.GetEnvironmentVariable("SemanticKernelAzureRegion", EnvironmentVariableTarget.User);
var azureKey = Environment.GetEnvironmentVariable("SemanticKernelAzureKey", EnvironmentVariableTarget.User);


////Using OpenAPI Key////
///
Kernel openAIKernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4o-mini-2024-07-18", $"{openAIKey}")
    .Build();

////Using Azure OpenAI Key////
//Kernel azureKernel = Kernel.CreateBuilder()
//    .AddAzureOpenAIChatCompletion
//    (
//    "gpt-4o-mini",
//    $"{azureRegion}",
//    $"{azureKey}")
//    .Build();


var options = new OpenAIPromptExecutionSettings
{
    MaxTokens = 10, // Set the maximum number of tokens to return
    Temperature = 0.7f // Set the temperature for response variability
};// Set as many properties you want


var prompt = "What is Semantic Kernel, describe briefly";//What whaterver you want LLM to return

var result = await openAIKernel.InvokePromptAsync(prompt, new KernelArguments(options)); //openaikernel

//var result = await azureKernel.InvokePromptAsync(prompt); // azurekernel

Console.WriteLine(result);