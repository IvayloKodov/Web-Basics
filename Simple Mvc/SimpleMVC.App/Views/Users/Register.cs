namespace SimpleMVC.App.Views.Users
{
    using System.Text;
    using MVC.Interfaces;

    public class Register : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<a href =\"/home/index\">&lt;Home</a>");
            sb.AppendLine("<main>");
            sb.AppendLine("<h2>Register new user</h2>");
            sb.AppendLine("<form action=\"\" method=\"post\">");
            sb.AppendLine("<label for=\"username\">Username: </label>");
            sb.AppendLine("<input type=\"text\" name=\"Username\" id=\"username\">");
            sb.AppendLine("<br>");
            sb.AppendLine("<label for=\"pass\">Password: </label>");
            sb.AppendLine("<input type=\"password\" name=\"Password\" id=\"pass\">");
            sb.AppendLine("<br>");
            sb.AppendLine("<input type=\"submit\" value=\"Register\">");
            sb.AppendLine("</form>");
            sb.AppendLine("</main>");

            return sb.ToString();
        }
    }
}