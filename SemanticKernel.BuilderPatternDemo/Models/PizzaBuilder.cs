namespace SemanticKernel.BuilderPatternDemo.Models;

public class PizzaBuilder
{
    private Pizza _pizza = new();
    public PizzaBuilder SetDough(string dough)
    {
        _pizza.Dough = dough;
        return this;
    }
    public PizzaBuilder SetSauce(string sauce)
    {
        _pizza.Sauce = sauce;
        return this;
    }

    public PizzaBuilder SetToppings(string toppings)
    {
        _pizza.Toppings = toppings;
        return this;
    }

    public Pizza Build()
    {
        return _pizza;
    }
}
