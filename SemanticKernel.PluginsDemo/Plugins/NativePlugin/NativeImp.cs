using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

namespace SemanticKernel.PluginsDemo.Plugins.FirstNativePlugin
{
    public class NativeImp
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
        public NativeImp(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }

        public async Task OpenAINative(OpenAIPromptExecutionSettings settings)
        {
            Kernel openAIKernel = Kernel.CreateBuilder()
              .AddOpenAIChatCompletion(_model, _key)
              .Build();
            //Plugin
            KernelPlugin conversationPlugin = KernelPluginFactory.CreateFromType<ConversationSummaryPlugin>();
            var summary = await openAIKernel.InvokeAsync(conversationPlugin.Where(x => x.Name == "SummarizeConversation").FirstOrDefault(),
                new() { { "input", "Semantic Kernel is a robust open-source framework designed to integrate artificial intelligence (AI) with application development seamlessly. It leverages large language models (LLMs) such as GPT or similar technologies, enabling developers to build applications that can comprehend, generate, and reason about textual content. The framework facilitates the creation of applications that blend natural language processing capabilities with traditional programming. At its core, Semantic Kernel introduces the concept of \"semantic memory,\" which allows applications to store and retrieve information in a way that mimics human cognition. This feature is instrumental in building intelligent systems capable of performing tasks like context-aware conversations, summarization, recommendation, and more.\r\n\r\nIn addition to its powerful AI capabilities, Semantic Kernel provides extensibility and flexibility to adapt to a wide range of use cases. Developers can design and manage workflows that dynamically adapt based on user input or real-time data, significantly enhancing user experience. By integrating seamlessly with existing tools and libraries, Semantic Kernel simplifies the AI development process, empowering teams to create solutions without requiring deep expertise in machine learning. Whether for creating advanced chatbots, automating content generation, or enhancing decision-making systems, Semantic Kernel represents a significant step forward in leveraging the potential of AI in software development" } });//Goto object browser, you may see all the functions for the plugin
            Console.WriteLine(summary);

            ////Plugin
            //KernelPlugin filePlugin = KernelPluginFactory.CreateFromType<FileIOPlugin>();
            //var writeFile = await openAIKernel.InvokeAsync(filePlugin.Where(x => x.Name == "Write").FirstOrDefault(),
            //                    new() { { "path", "Your txt file path" }, { "content", "This is a demo" } });
            //Console.WriteLine(writeFile);
            //var read = await openAIKernel.InvokeAsync(filePlugin.Where(x => x.Name == "Read").FirstOrDefault(),
            //                    new() { { "path", "Your txt file path" } });
            //Console.WriteLine(read);


            ////Plugin
            //var httpPlugin = KernelPluginFactory.CreateFromType<HttpPlugin>();
            //var httpGet = await openAIKernel.InvokeAsync(httpPlugin.Where(x => x.Name == "Get").FirstOrDefault(),
            //                    new() { { "uri", "https://jsonplaceholder.typicode.com/posts/1" } });
            //Console.WriteLine(httpGet);

            
            ////Plugin
            //KernelPlugin timePlugin = KernelPluginFactory.CreateFromType<TimePlugin>();
            //var time = await openAIKernel.InvokeAsync(timePlugin.Where(x => x.Name == "Time").FirstOrDefault(),
            //                    new() { });
            //Console.WriteLine(httpGet);

            
            ////Plugin
            //KernelPlugin textPlugin = KernelPluginFactory.CreateFromType<TextPlugin>();
            //var concat = await openAIKernel.InvokeAsync(textPlugin.Where(x => x.Name == "Concat").FirstOrDefault(),
            //                    new() { { "input", "Hello " }, { "input2", "World" } });
            //Console.WriteLine(concat);


        }


    }
}
