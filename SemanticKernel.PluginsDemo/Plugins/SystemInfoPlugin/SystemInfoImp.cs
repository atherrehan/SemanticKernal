using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace SemanticKernel.PluginsDemo.Plugins.SystemInfoPlugin
{
    public class SystemInfoImp
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
        public SystemInfoImp(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAIGetMemory(OpenAIPromptExecutionSettings settings)
        {
            KernelPlugin systemInfoPlugin = KernelPluginFactory.CreateFromType<SystemInfoPlugin>();

            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAIChatCompletion(_model, _key)
               .Build();

            var systemInfor = await openAIKernel.InvokeAsync(systemInfoPlugin.Where(x => x.Name == "get_memory_ram").FirstOrDefault());

            Console.WriteLine(systemInfor);
        }

        public async Task OpenAIGetTopMemory(OpenAIPromptExecutionSettings settings, int processess)
        {
            KernelPlugin systemInfoPlugin = KernelPluginFactory.CreateFromType<SystemInfoPlugin>();

            Kernel openAIKernel = Kernel.CreateBuilder()
               .AddOpenAIChatCompletion(_model, _key)
               .Build();

            var systemInfor = await openAIKernel.InvokeAsync(systemInfoPlugin.Where(x => x.Name == "get_top_memory_processes").FirstOrDefault(),
                new KernelArguments() { { "processes", processess } });

            Console.WriteLine(systemInfor);
        }


    }
}
