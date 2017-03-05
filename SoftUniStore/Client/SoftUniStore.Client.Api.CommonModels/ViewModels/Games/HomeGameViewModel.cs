namespace SoftUniStore.Client.Api.CommonModels.ViewModels.Games
{
    using System.Linq;
    using System.Text;

    public class HomeGameViewModel
    {
        public int GameId { get; set; }

        public string Thumbnail { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<div class=\"card col-4 thumbnail\">");
            sb.Append($"<img class=\"card-image-top img-fluid img-thumbnail\" src={this.Thumbnail}>");
            sb.Append("<div class=\"card-block\">");
            sb.Append($"<h4 class=\"card-title\">{this.Title}</h4>");
            sb.Append($"<p class=\"card-text\"><strong>Price</strong> - {this.Price}&euro;</p>");
            sb.Append($"<p class=\"card-text\"><strong>Size</strong> - {this.Size} GB</p>");

            if (this.Description.Length > 297)
            {
                sb.Append($"<p class=\"card-text\">{this.Description.Substring(0, 297)}...</p>");
            }

            sb.Append($"<p class=\"card-text\">{this.Description}</p>");
            sb.Append("</div>");
            sb.Append("<div class=\"card-footer\">");
            sb.Append($"<a class=\"card - button btn btn - outline - primary\" name=\"info\" href=\"/home/gameDetails?gameId={this.GameId}\">Info</a>");
            sb.Append("</div>");
            sb.Append("</div>");

            return sb.ToString();
        }
    }
}