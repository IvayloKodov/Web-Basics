namespace SoftUniStore.Client.Api.Common.Readers
{
    using System.IO;

    public class HtmlReader
    {
        public static string ReadHtml(string path)
        {
            return File.ReadAllText(path);
        }
    }
}