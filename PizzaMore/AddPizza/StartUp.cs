namespace AddPizza
{
    using System;
    using System.Linq;
    using PizzaMore.Data;
    using PizzaMore.Models;
    using PizzaMore.Utility;
    using PizzaMore.Utility.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            PizzaMoreContext context = new PizzaMoreContext();
            IFileReader fileReader = new FileReader();
            AddPizza addPizza = new AddPizza(context, fileReader);

            if (addPizza.Session == null)
            {
                Console.WriteLine(addPizza.Header);
                WebUtils.PageNotAllowed();
            }
            else
            {
                if (WebUtils.IsPost())
                {
                    var parameters = WebUtils.RetrievePostParameters();

                    var pizzaTitle = parameters.ContainsKey("title") ? parameters["title"] : null;
                    var pizzaRecipe = parameters.ContainsKey("recipe") ? parameters["recipe"] : null;
                    var pizzaUrl = parameters.ContainsKey("url") ? parameters["url"] : null;

                    var user = context.Users
                        .FirstOrDefault(u => u.Id == addPizza.Session.UserId);
                
                    Pizza pizza = new Pizza
                    {
                        Title = pizzaTitle,
                        Recipe = pizzaRecipe,
                        ImageUrl = pizzaUrl,
                        OwnerId = user.Id
                    };

                    user.Suggestions.Add(pizza);
                    context.SaveChanges();
                }

                PrintAddPizzaHtml(addPizza);
            }
        }

        private static void PrintAddPizzaHtml(AddPizza addPizza)
        {
            Console.WriteLine(addPizza.Header);
            WebUtils.PrintFileContent("../../htdocs/PizzaLab/addpizza.html");
        }
    }
}
