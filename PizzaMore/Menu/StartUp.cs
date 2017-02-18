namespace Menu
{
    using System;
    using PizzaMore.Data;
    using PizzaMore.Utility;
    using PizzaMore.Utility.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            PizzaMoreContext context = new PizzaMoreContext();
            IFileReader fileReader = new FileReader();
            Header header = new Header();

            Menu menu = new Menu(context, fileReader);

            if (menu.Session == null)
            {
                Console.WriteLine(header);
                WebUtils.PageNotAllowed();
            }
            else
            {
                Console.WriteLine(menu);
            }
        }
    }
}
