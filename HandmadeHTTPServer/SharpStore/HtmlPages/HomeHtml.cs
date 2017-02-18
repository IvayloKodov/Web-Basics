namespace SharpStore.HtmlPages
{
    using System.IO;
    using Contracts;
    public class HomeHtml :IHtml
    {
        public string Print(string url)
        {
            return File.ReadAllText("../../Content/home.html");
        }
    }
}