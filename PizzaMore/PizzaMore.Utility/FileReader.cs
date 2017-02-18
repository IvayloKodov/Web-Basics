namespace PizzaMore.Utility
{
    using System.IO;
    using Contracts;

    public class FileReader : IFileReader
    {
        public string ReadHtml(string path)
        {
            return File.ReadAllText(path);
        }
    }
}