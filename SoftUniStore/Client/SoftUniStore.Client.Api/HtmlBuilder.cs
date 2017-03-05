namespace SoftUniStore.Client.Api
{
    using System.Text;
    using Common.Constants;
    using Common.Providers;

    public class HtmlBuilder
    {
        public static string GetHtmlTemplate(string content)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(HtmlsProvider.htmls[HtmlNames.Header]);
            sb.Append(content);
            sb.Append(HtmlsProvider.htmls[HtmlNames.Footer]);

            return sb.ToString();
        }
    }
}