using Microsoft.SemanticKernel.Connectors.OpenAI;

#region Declaration
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var openAIKey = Environment.GetEnvironmentVariable("SemanticKernelOpenAIKey", EnvironmentVariableTarget.User);

var azureRegion = Environment.GetEnvironmentVariable("SemanticKernelAzureRegion", EnvironmentVariableTarget.User);

var azureKey = Environment.GetEnvironmentVariable("SemanticKernelAzureKey", EnvironmentVariableTarget.User);

var modelIdOpenAI = "gpt-4o-mini-2024-07-18";

var options = new OpenAIPromptExecutionSettings // Set as many properties you want
{
    MaxTokens = 50,                             // Set the maximum number of tokens to return
    Temperature = 0.7f                          // Set the temperature for response variability
};

OpenAITextToAudioExecutionSettings audioOptions = new OpenAITextToAudioExecutionSettings // See platform.openai docs for futher configuration options
{
    Voice = "alloy",                            // Set the voice for text-to-audio conversion
    ResponseFormat = "mp3"                      // Set the audio format
};


OpenAIAudioToTextExecutionSettings textOptions = new OpenAIAudioToTextExecutionSettings // See platform.openai docs for futher configuration options
{
    Language = "en",                            // Set the language en,ar etc
    Prompt = "This is some prompt",
    ResponseFormat = "json",
    Temperature = 0.3f,                         // Set the temperature for response variability
};

var prompt = string.Empty; // What whaterver you want LLM to return

#endregion

#region Chats

//prompt = "What is Semantic Kernel, describe briefly";
//SKChat chat = new SKChat(modelIdOpenAI, openAIKey ?? "", prompt, "");
////await chat.OpenAIChat(options);
////await chat.OpenAIChatStreaming(options);

//SKChat chat = new SKChat(modelIdOpenAI, azureKey ?? "", prompt, azureRegion);
////await chat.AzureAIChat(options);
////await chat.AzureAIChatStreaming(options);

#endregion

#region Images

//prompt = "A futuristic city skyline at sunset with flying cars.";
//SKImage image = new SKImage(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await image.OpenAIImage();

//modelIdOpenAI = "dall-e-3";//Deploy base model in Azure
//SKImage image = new SKImage(modelIdOpenAI, azureKey ?? "", prompt, azureRegion ?? "");
//await image.AzureAIImage();

#endregion

#region Text 2 Audio

//var path = "D:\\Ather's Workspace\\GitRepo\\atherrehan\\SemanticKernal\\Audio";
//modelIdOpenAI = "tts-1";
//prompt = "Hello there! How is it going with Microsoft Semantic Kernel?";
//SKTextToAudio audio = new SKTextToAudio(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await audio.OpenAIAudio(audioOptions, path);
//SKTextToAudio audio = new SKTextToAudio(modelIdOpenAI, azureKey ?? "", prompt, azureRegion ?? "");
//await audio.AzureAIAudio(audioOptions, path);

#endregion

#region Audio 2 Text

//var path = "D:\\Ather's Workspace\\GitRepo\\atherrehan\\SemanticKernal\\Audio\\b3e40290-065d-411d-86e4-022ae43b0c48.mp3";
//modelIdOpenAI = "whisper-1";
//SKAudioToText text = new SKAudioToText(modelIdOpenAI, openAIKey ?? "", prompt, "");
//await text.OpenAIText(textOptions, path);
//SKAudioToText text = new SKAudioToText(modelIdOpenAI, azureKey ?? "", prompt, azureRegion ?? "");
//await text.AzureAIText(audioOptions, path);

#endregion
