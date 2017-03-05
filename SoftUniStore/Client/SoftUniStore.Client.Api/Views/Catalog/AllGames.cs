namespace SoftUniStore.Client.Api.Views.Catalog
{
    using System.Collections.Generic;
    using System.Text;
    using Common.Constants;
    using CommonModels.ViewModels.Games;
    using SimpleMVC.Interfaces.Generic;
    using static Common.Providers.HtmlsProvider;

    public class AllGames : IRenderable<IEnumerable<AdminGameViewModel>>
    {
        public string Render()
        {
            var sb = new StringBuilder();
            sb.Append(htmls[HtmlNames.NavLogged]);
            sb.Append(htmls[HtmlNames.AdminGames].Replace("{AdminCategories}", string.Join("", this.Model)));

            return HtmlBuilder.GetHtmlTemplate(sb.ToString());
        }

        public IEnumerable<AdminGameViewModel> Model { get; set; }
    }
}