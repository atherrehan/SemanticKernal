using SemanticKernel.BuilderPatternDemo.Models;

////Example 1 by envoking all methods////
///

var builder = new PizzaBuilder()
    .SetDough("Thin Crust")
    .SetSauce("Tomato")
    .SetToppings("Pepperoni, Mushrooms, Olives")
    .Build();


////Example 2 by envoking selected methods////
///

//var builder = new PizzaBuilder()
//    .SetDough("Thin Crust")
//    .SetToppings("Pepperoni, Mushrooms, Olives")
//    .Build();

Console.WriteLine(builder);