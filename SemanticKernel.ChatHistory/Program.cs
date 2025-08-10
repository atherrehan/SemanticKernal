#region Declaration

using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
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

ChatHistory history = [];

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

#region Chat with mulitple prompts and histroy

//prompt = "What is my name?";
//history.AddSystemMessage("You are a helpful assistant that remembers the user's name.");
//history.AddSystemMessage("Welcome! Please tell me your name to get started.");
//history.AddUserMessage("My name is Ather");
//history.AddAssistantMessage("Nice to meet you, Ather! How can I assist you today?");
//history.AddUserMessage(prompt);
//ChatDemo openAIChatDemo = new ChatDemo(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await openAIChatDemo.OpenAIChatCompletion(options, history);

////With SK we can keep the chat histroy which we will do in next region
////This example is simple with added UserMessge, in next region we will be doing this with proper chat

#endregion


#region Chat with mulitple prompts and histroy (Dynamic/Multi-model)
history = new ChatHistory("You are an expert at obtaining object tags from an image");
//From URL
history.AddUserMessage([
    new Microsoft.SemanticKernel.TextContent("Give me the tags of the image"),
    new ImageContent( new Uri("https://www.telegraph.co.uk/content/dam/Travel/Destinations/North%20America/USA/New%20York/newyork-skyline-GettyImages-1347979016.jpg"))
    ]);


////From Local machine
//var imageBytes = File.ReadAllBytes("your path");
//history.AddUserMessage([
//    new Microsoft.SemanticKernel.TextContent("Give me the tags of the image"),
//    new ImageContent( imageBytes,"image/jpeg")
//    ]);

ChatDemo openAIChatDemo = new ChatDemo(modelIdOpenAI, openAIKey ?? "", prompt, "");
await openAIChatDemo.OpenAIChatCompletion(options, history);

//With SK we can keep the chat histroy which we will do in next region
//This example is simple with added UserMessge, in next region we will be doing this with proper chat

#endregion