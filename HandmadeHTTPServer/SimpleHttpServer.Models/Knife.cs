namespace SimpleHttpServer.Models
{
    public class Knife
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public override string ToString()
        {
            return 
                   $"<div class=\"col-md-4\">\r\n" +
                   $"<div class=\"img-thumbnail\">\r\n" +
                   $"<img src=\"{this.ImgUrl}\">\r\n " +
                   $"<div class=\"caption\">\r\n" +
                   $"<h2>{this.Name}</h2>\r\n" +
                   $"<p>${this.Price}</p>\r\n" +
                   $"</div>\r\n" +
                   $"<input type=\"submit\" value=\"Buy Now\" class=\"btn btn-primary\">\r\n" +
                   $"</div>\r\n" +
                   $"</div>";
        }
    }
}