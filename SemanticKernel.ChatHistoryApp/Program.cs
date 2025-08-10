using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.ChatHistoryApp;
using Spectre.Console;
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

ChatHistory history;

#endregion

#region Demo

history = new ChatHistory("You are a helpful assitant");

ChatHistoryApp openAIChatDemo = new ChatHistoryApp(modelIdOpenAI, openAIKey ?? "", prompt, "");

openAIChatDemo.ShowWelcomeMessages();

#endregion