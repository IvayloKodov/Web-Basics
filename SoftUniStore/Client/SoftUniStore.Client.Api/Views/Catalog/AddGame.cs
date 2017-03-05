namespace SoftUniStore.Client.Api.Views.Catalog
{
    using System.Text;
    using Common.Constants;
    using SimpleMVC.Interfaces;
    using static Common.Providers.HtmlsProvider;

    public class AddGame : IRenderable
    {
        public string Render()
        {
           var sb = new StringBuilder();
            sb.Append(htmls[HtmlNames.NavLogged]);
            sb.Append(htmls[HtmlNames.AddGame]);

            return HtmlBuilder.GetHtmlTemplate(sb.ToString());
        }
    }
}