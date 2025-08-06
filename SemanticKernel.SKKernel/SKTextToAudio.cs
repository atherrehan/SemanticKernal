using Microsoft.SemanticKernel.TextToImage;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToAudio;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SemanticKernel.SKKernel
{
    public class SKTextToAudio
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public SKTextToAudio(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAIAudio(OpenAITextToAudioExecutionSettings settings, string path = "")
        {

            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAITextToAudio(_model, _key)
               .Build();
            var service = openAIKernel.GetRequiredService<ITextToAudioService>();
            var audio = await service.GetAudioContentAsync(_prompt, settings);
            var audioFilePath = Path.Combine(path, $"{Guid.NewGuid()}.mp3"); // Generate a unique file name
            await File.WriteAllBytesAsync(audioFilePath, audio.Data!.Value.ToArray());
            Console.WriteLine($"Audio saved to: {audioFilePath}");
        }

        public async Task AzureAIAudio(OpenAITextToAudioExecutionSettings settings, string path = "")
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
              .AddAzureOpenAIAudioToText(_model, endpoint: _region, apiKey: _key)
              .Build();
            var service = openAIKernel.GetRequiredService<ITextToAudioService>();
            var audio = await service.GetAudioContentAsync(_prompt, settings);
            var audioFilePath = Path.Combine(path, $"{Guid.NewGuid()}.mp3"); // Generate a unique file name
            await File.WriteAllBytesAsync(audioFilePath, audio.Data!.Value.ToArray());
            Console.WriteLine($"Audio saved to: {audioFilePath}");

        }
    }
}
