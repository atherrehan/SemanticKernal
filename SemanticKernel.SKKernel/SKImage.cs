using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;

namespace SemanticKernel.SKKernel
{
    public class SKImage
    {
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;

        public SKImage(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAIImage()
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
                .AddOpenAITextToImage(_key)
                .Build();
            var service = openAIKernel.GetRequiredService<ITextToImageService>();
            var images = await service.GetImageContentsAsync(_prompt);
            Console.WriteLine(images[0].Uri!.ToString());
        }

        public async Task AzureAIImage()
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
                .AddAzureOpenAITextToImage(_model, endpoint: _region, apiKey: _key)
                .Build();
            var service = openAIKernel.GetRequiredService<ITextToImageService>();
            var images = await service.GetImageContentsAsync(_prompt);
            Console.WriteLine(images[0].Uri!.ToString());

        }
    }
}
