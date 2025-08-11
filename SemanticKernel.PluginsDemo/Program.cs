using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.PluginsDemo;

#region Declaration

var openAIKey = Environment.GetEnvironmentVariable("SemanticKernelOpenAIKey", EnvironmentVariableTarget.User);

var azureRegion = Environment.GetEnvironmentVariable("SemanticKernelAzureRegion", EnvironmentVariableTarget.User);

var azureKey = Environment.GetEnvironmentVariable("SemanticKernelAzureKey", EnvironmentVariableTarget.User);

var modelIdOpenAI = "gpt-4o-mini-2024-07-18";

var options = new OpenAIPromptExecutionSettings   // Set as many properties you want
{
    MaxTokens = 4000,                             // Set the maximum number of tokens to return
    Temperature = 0.7f                            // Set the temperature for response variability
};

var prompt = string.Empty; // What whaterver you want LLM to return

#endregion


#region Demo



FunctionDemo demo = new FunctionDemo(modelIdOpenAI, openAIKey??"", "", "");
await demo.OpenAIChatInitialize(options);
#endregion