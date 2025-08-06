using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKBlogPostsGeneration;
using Spectre.Console;

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

string topic = string.Empty;

string prompt = string.Empty; // What whaterver you want LLM to return

#endregion

#region Blog Post

topic = AnsiConsole.Ask<string>("What is the [green]topic[/] for your post?");
prompt = $@"Generate a detailed blog post about '{topic}'.
Include an introdution, multiple paragraphs, some code snippets and finally, a conclusion.
Seperate each section with a heading.
It's mandatory that you use the following Gutenberg blocks for a wordpress site:
Heading
<!-- wp:heading {{""level"":1,""fontSize"":""level-6""}} --> <h1 class=""wp-block-heading has-level-6-font-size"">Say Hello to Gutenberg, the WordPress Editor</h1> <!-- /wp:heading --> *Modify `""level"":1` and `""fontSize"":""level-6""` according to the desired heading level and size.*
Paragraph
<!-- wp:paragraph --> <p>This is a sample paragraph in Gutenberg. You can add more text here to show how a paragraph block would look.</p> <!-- /wp:paragraph -->
Unordered List
<!-- wp:list --> <ul> <li>List item 1</li> <li>List item 2</li> <li>List item 3</li> </ul> <!-- /wp:list -->
Ordered List
<!-- wp:list {{""ordered"":true}} --> <ol> <li>First item</li> <li>Second item</li> <li>Third item</li> </ol> <!-- /wp:list -->
Quote
<!-- wp:quote --> <blockquote class=""wp-block-quote""> <p>""Experience is simply the name we give our mistakes."" – Oscar Wilde</p> <cite>Oscar Wilde</cite> </blockquote> <!-- /wp:quote -->
Code
<!-- wp:code --> <pre class=""wp-block-code""><code>function helloWorld() {{ console.log(""Hello, world!""); }}</code></pre> <!-- /wp:code -->
Bold Text and Links Within a Paragraph
<!-- wp:paragraph --> <p>This is a <strong>bold text</strong> with a <a href=""https://example.com"">link</a> example.</p> <!-- /wp:paragraph -->
Italic and Underlined Text
<!-- wp:paragraph --> <p>This is an <em>italic text</em> and <u>underlined</u> within a paragraph.</p> <!-- /wp:paragraph -->
";



TextBlog textBlog = new TextBlog(modelIdOpenAI, openAIKey ?? "", prompt, "");

await textBlog.OpenAITextBlog();

#endregion
