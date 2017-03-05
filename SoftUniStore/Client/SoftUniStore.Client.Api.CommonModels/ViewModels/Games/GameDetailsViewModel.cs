namespace SoftUniStore.Client.Api.CommonModels.ViewModels.Games
{
    using System;
    using System.Text;

    public class GameDetailsViewModel
    {
        public int GameId { get; set; }

        public string Title { get; set; }

        public string YouTubeId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public DateTime ReleaseDate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"<h1 class=\"display-3\">{this.Title}</h1>");
            sb.Append("<br/>");
            sb.Append($"<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/{this.YouTubeId}\" frameborder=\"0\" allowfullscreen></iframe>");
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append($"<p>{this.Description}</p>");
            sb.Append($"<p><strong>Price</strong> - {this.Price}&euro;</p>");
            sb.Append($"<p><strong>Size</strong> - {this.Size} GB</p>");
            sb.Append($"<p><strong>Release Date</strong> - {this.ReleaseDate} AM</p>");
            sb.Append("<a class=\"btn btn-outline-primary\" name=\"back\" href=\"/home/all\">Back</a>");
            sb.Append("<form action=\"/home/BuyGame\" method=\"post\">");
            sb.Append($"<input name=\"gameId\" type=\"hidden\" value=\"{this.GameId}\"/>");
            sb.Append("<br />");
            sb.Append("<input type=\"submit\" class=\"btn btn-success\" value=\"Buy\" />");
            sb.Append("</form");

            return sb.ToString();
        }
    }
}