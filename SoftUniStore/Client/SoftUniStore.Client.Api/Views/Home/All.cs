namespace SoftUniStore.Client.Api.Views.Home
{
    using System.Text;
    using Common.Constants;
    using CommonModels.ViewModels.Games;
    using SimpleMVC.Interfaces.Generic;
    using static Common.Providers.HtmlsProvider;

    public class All : IRenderable<AllHomeGamesViewModel>
    {
        public string Render()
        {
            var sb = new StringBuilder();
            sb.Append(htmls[HtmlNames.NavLogged]);
            sb.Append(htmls[HtmlNames.Home].Replace("{Games}", string.Join("", this.Model)));

            return HtmlBuilder.GetHtmlTemplate(sb.ToString());
        }

        public AllHomeGamesViewModel Model { get; set; }
    }
}