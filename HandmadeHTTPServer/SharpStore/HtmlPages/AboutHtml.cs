namespace SharpStore.HtmlPages
{
    using System.IO;
    using Contracts;

    public class AboutHtml : IHtml
    {
        public string Print(string url)
        {
            return File.ReadAllText("../../Content/about.html");
        }
    }
}