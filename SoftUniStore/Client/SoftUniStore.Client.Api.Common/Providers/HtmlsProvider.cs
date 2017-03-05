namespace SoftUniStore.Client.Api.Common.Providers
{
    using System.Collections.Generic;
    using System.IO;
    using Readers;
    using SoftUniStore.Client.Api.Common.Constants;

    public static class HtmlsProvider
    {
        public static readonly Dictionary<string, string> htmls = new Dictionary<string, string>();

        public static void Initialize()
        {
            var allContentHtmls = Directory.GetFiles("../../content");

            foreach (var htmlPath in allContentHtmls)
            {
                var htmlName = htmlPath.Substring(htmlPath.LastIndexOf(@"\") + 1);
                htmls.Add(htmlName, HtmlReader.ReadHtml(htmlPath));
            }
        }
    }
}