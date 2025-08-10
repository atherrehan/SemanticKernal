#region Declaration

using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.ChatHistoryDemo;

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

#region Simple chat

//prompt = "What is the capital of Oman?";

//ChatDemo openAIChatDemo = new ChatDemo(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await openAIChatDemo.OpenAIChatCompletion(options);

#endregion

#region Chat with will mulitple prompts

//prompt = "My name is Ather?";

//ChatDemo openAIChatDemo = new ChatDemo(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await openAIChatDemo.OpenAIChatCompletion(options);

//prompt = "What is my name?";

//openAIChatDemo = new ChatDemo(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await openAIChatDemo.OpenAIChatCompletion(options); // Once you do this, you will notice it will have information regarding your name

//With SK we can keep the chat histroy which we will do in next region

#endregion

#region Chat with will mulitple prompts and histroy

prompt = "What is my name?";
ChatHistory history = [];
history.AddSystemMessage("You are a helpful assistant that remembers the user's name.");
history.AddSystemMessage("Welcome! Please tell me your name to get started.");
history.AddUserMessage("My name is Ather");
history.AddAssistantMessage("Nice to meet you, Ather! How can I assist you today?");
history.AddUserMessage(prompt);
ChatDemo openAIChatDemo = new ChatDemo(modelIdOpenAI, openAIKey ?? "", prompt, "");
await openAIChatDemo.OpenAIChatCompletion(options, history);

//With SK we can keep the chat histroy which we will do in next region

#endregion