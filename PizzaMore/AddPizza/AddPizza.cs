namespace AddPizza
{
    using PizzaMore.Data;
    using PizzaMore.Models;
    using PizzaMore.Utility;
    using PizzaMore.Utility.Contracts;

    public class AddPizza
    {
        private readonly Header header;
        private readonly Session session;
        private readonly IFileReader fileReader;
        private readonly PizzaMoreContext context;

        public AddPizza(PizzaMoreContext pizzaMoreContext, IFileReader reader)
        {
            this.fileReader = reader;
            this.header = new Header();
            this.context = pizzaMoreContext;
            this.session = WebUtils.GetSession(this.context);
        }

        public Session Session => this.session;
        public Header Header => this.header;
    }
}