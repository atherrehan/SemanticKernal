using Microsoft.SemanticKernel;

namespace SemanticKernel.PluginsDemo.Plugins.Functions
{
    public class FunctionsPlugins
    {

        public KernelFunction timeFunction = KernelFunctionFactory.CreateFromMethod(() =>
           DateTime.Now.ToShortTimeString(),
           "get_current_time",
           "Gets the current time"
           );

        public KernelFunction poemFunction = KernelFunctionFactory.CreateFromPrompt(
             "Write a poem about Semantic",
             functionName: "wirte_poem",
             description: "Writes a poem about Semantic Kernel"
             );
    }
}
