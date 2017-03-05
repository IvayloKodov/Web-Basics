namespace SoftUniStore.Client.Api.CommonModels.ViewModels.Games
{
    using System.Text;

    public class AdminGameViewModel
    {
        public int GameId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append($"<td>{this.Title}</td>");
            sb.Append($"<td>{this.Size} GB</td>");
            sb.Append($"<td>{this.Price} &euro;</td>");
            sb.Append("<td>");
            sb.Append($"<a href=\"/catalog/EditGame?gameId={this.GameId}\" class=\"btn btn-warning btn-sm\">Edit</a>");
            sb.Append($"<a href=\"/catalog/DeleteGame?gameId={this.GameId}\" class=\"btn btn-danger btn-sm\">Delete</a>");
            sb.Append("</td>");
            sb.Append("</tr>");

            return sb.ToString();
        }
    }
}