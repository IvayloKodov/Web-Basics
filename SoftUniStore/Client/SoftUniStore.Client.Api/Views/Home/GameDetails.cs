namespace SoftUniStore.Client.Api.Views.Home
{
    using System.Text;
    using Common.Constants;
    using CommonModels.ViewModels.Games;
    using SimpleMVC.Interfaces.Generic;
    using static Common.Providers.HtmlsProvider;

    public class GameDetails : IRenderable<GameDetailsViewModel>
    {
        public string Render()
        {
            var sb = new StringBuilder();
            sb.Append(htmls[HtmlNames.NavLogged]);
            sb.Append(htmls[HtmlNames.GameDetails].Replace("{GameDetails}", this.Model.ToString()));
            return HtmlBuilder.GetHtmlTemplate(sb.ToString());
        }

        public GameDetailsViewModel Model { get; set; }
    }
}