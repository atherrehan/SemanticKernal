using Microsoft.SemanticKernel;

var openAIKey = Environment.GetEnvironmentVariable("SemanticKernelOpenAIKey",EnvironmentVariableTarget.User);
//var azureRegion = Environment.GetEnvironmentVariable("SemanticKernelAzureRegion");
//var azureKey = Environment.GetEnvironmentVariable("SemanticKernelAzureKey");

Kernel openAIKernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4o-mini-2024-07-18", $"{openAIKey}")
    .Build();


//Kernel azureKernel = Kernel.CreateBuilder()
//    .AddAzureOpenAIChatCompletion
//    (
//    "gpt-4o-mini",
//    $"{azureRegion}",
//    $"{azureKey}")
//    .Build();


var prompt = "What is Semantic Kernel, describe briefly";//What whaterver you want LLM to return

var result = await openAIKernel.InvokePromptAsync(prompt);

Console.WriteLine(result);