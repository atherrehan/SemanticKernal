using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.TextToAudio;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AudioToText;

namespace SemanticKernel.SKKernel
{
    public class SKAudioToText
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public SKAudioToText(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAIText(OpenAIAudioToTextExecutionSettings settings, string path = "")
        {

            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAIAudioToText(_model, _key)
               .Build();
            var service = openAIKernel.GetRequiredService<IAudioToTextService>();
            var audioStream = File.OpenRead(path);
            var audioFileBinaryData = await BinaryData.FromStreamAsync(audioStream);
            AudioContent audioContent = new(audioFileBinaryData, mimeType: null);
            var text = await service.GetTextContentAsync(audioContent, settings);
            Console.WriteLine($"Text saved to: {text.Text}");
        }

        public async Task AzureAIText(OpenAIAudioToTextExecutionSettings settings, string path = "")
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddAzureOpenAIAudioToText(_model, endpoint: _region, apiKey: _key)
               .Build();
            var service = openAIKernel.GetRequiredService<IAudioToTextService>();
            var audioStream = File.OpenRead(path);
            var audioFileBinaryData = await BinaryData.FromStreamAsync(audioStream);
            AudioContent audioContent = new(audioFileBinaryData, mimeType: null);
            var text = await service.GetTextContentAsync(audioContent, settings);
            Console.WriteLine($"Text saved to: {text.Text}");

        }
    }
}
