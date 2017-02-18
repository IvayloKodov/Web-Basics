namespace Menu
{
    using System.Linq;
    using System.Text;
    using PizzaMore.Data;
    using PizzaMore.Models;
    using PizzaMore.Utility;
    using System.Collections.Generic;
    using PizzaMore.Utility.Contracts;

    public class Menu
    {
        private readonly Header header;
        private readonly Session session;
        private readonly IFileReader fileReader;
        private readonly PizzaMoreContext context;

        public Menu(PizzaMoreContext pizzaMoreContext, IFileReader reader)
        {
            this.header = new Header();
            this.fileReader = reader;
            this.context = pizzaMoreContext;
            this.session = WebUtils.GetSession(this.context);
        }

        public Session Session => this.session;

        private string GenerateNavbar()
        {
            string navBarHtml = "<nav class=\"navbar navbar-default\">" +
                "<div class=\"container-fluid\">" +
                "<div class=\"navbar-header\">" +
                "<a class=\"navbar-brand\" href=\"Home.exe\">PizzaMore</a>" +
                "</div>" +
                "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">" +
                "<ul class=\"nav navbar-nav\">" +
                "<li ><a href=\"AddPizza.exe\">Suggest Pizza</a></li>" +
                "<li><a href=\"YourSuggestions.exe\">Your Suggestions</a></li>" +
                "</ul>" +
                "<ul class=\"nav navbar-nav navbar-right\">" +
                "<p class=\"navbar-text navbar-right\"></p>" +
                "<p class=\"navbar-text navbar-right\"><a href=\"Home.exe?logout=true\" class=\"navbar-link\">Sign Out</a></p>" +
                $"<p class=\"navbar-text navbar-right\">Signed in as {this.session.User.Email}</p>" +
                "</ul> </div></div></nav>";

            return navBarHtml;
        }

        private string GenerateAllSuggestions()
        {
            IEnumerable<Pizza> pizzas = this.context.Pizzas.ToList();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<div class=\"card-deck\">");
            foreach (var pizza in pizzas)
            {
                sb.AppendLine("<div class=\"card\">");
                sb.AppendLine($"<img class=\"card-img-top\" src=\"{pizza.ImageUrl}\" width=\"200px\"alt=\"Card image cap\">");
                sb.AppendLine("<div class=\"card-block\">");
                sb.AppendLine($"<h4 class=\"card-title\">{pizza.Title}</h4>");
                sb.AppendLine($"<p class=\"card-text\"><a href=\"DetailsPizza.exe?pizzaid={pizza.Id}\">Recipe</a></p>");
                sb.AppendLine("<form method=\"POST\">");
                sb.AppendLine("<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"up\">Up</label></div>");
                sb.AppendLine("<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"down\">Down</label></div>");
                sb.AppendLine($"<input type=\"hidden\" name=\"pizzaid\" value=\"{pizza.Id}\" />");
                sb.AppendLine("<input type=\"submit\" class=\"btn btn-primary\" value=\"Vote\" />");
                sb.AppendLine("</form>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }
            sb.AppendLine("</div>");

            return sb.ToString();
        }

        private string GetMenuTopHtml()
        {
            return this.fileReader.ReadHtml("../../htdocs/PizzaLab/menu-top.html");
        }

        private string GetMenuBottomHtml()
        {
            return this.fileReader.ReadHtml("../../htdocs/PizzaLab/menu-bottom.html");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.header.ToString());
            sb.AppendLine(this.GetMenuTopHtml());
            sb.AppendLine(this.GenerateNavbar());
            sb.AppendLine(this.GenerateAllSuggestions());
            sb.AppendLine(this.GetMenuBottomHtml());

            return sb.ToString();
        }
    }
}