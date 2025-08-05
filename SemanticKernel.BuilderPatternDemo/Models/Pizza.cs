namespace SemanticKernel.BuilderPatternDemo.Models
{
    public class Pizza
    {
        public string? Dough { get; set; }
        public string? Sauce { get; set; }
        public string? Toppings { get; set; }
        public override string ToString()
        {
            return $"Pizza with {Dough} dough, {Sauce} sauce, and {Toppings} toppings.";
        }
    }


}
