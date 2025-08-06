using Microsoft.SemanticKernel;
using Spectre.Console;

namespace SKBlogPostsGeneration
{
    public class TextBlog
    {
        private readonly string _model;
        private readonly string _key;
        private readonly string _prompt;
        private readonly string _region;
        public TextBlog(string model, string key, string prompt, string region)
        {
            _key = key;
            _model = model;
            _prompt = prompt;
            _region = region;
        }
        public async Task OpenAITextBlog()
        {
            AnsiConsole.MarkupLine("[bold yellow][/] GENERATING BLOG POST....");
            var kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(_model, _key).Build();
            string blogContent = string.Empty;
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Arrow)
                .StartAsync("Generating blog post...", async ctx =>
                {
                    var result = await kernel.InvokePromptAsync(_prompt);
                    blogContent = result.ToString();
                });
            AnsiConsole.MarkupLine("[green] BLOG POST GENERATED SUCCESSFULLY![/]");
            AnsiConsole.Write(new Text(blogContent));

        }
    }
}
