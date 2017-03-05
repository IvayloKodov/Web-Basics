namespace SoftUniStore.Client.Api.Views.Catalog
{
    using System.Text;
    using Common.Constants;
    using CommonModels.ViewModels.Games;
    using SimpleMVC.Interfaces.Generic;
    using static Common.Providers.HtmlsProvider;

    public class EditGame : IRenderable<EditGameViewModel>
    {
        public string Render()
        {
            var sb = new StringBuilder();
            sb.Append(htmls[HtmlNames.NavLogged]);
            var editHtml = htmls[HtmlNames.EditGame];
            editHtml = editHtml.Replace("{id}", this.Model.Id.ToString())
                    .Replace("{title}", this.Model.Title)
                    .Replace("{description}", this.Model.Description)
                    .Replace("{thumbnail}", this.Model.ImageThumbnail)
                    .Replace("{price}", this.Model.Price.ToString())
                    .Replace("{size}", this.Model.Size.ToString())
                    .Replace("{trailer}", this.Model.Trailer);

            sb.Append(editHtml);

            return HtmlBuilder.GetHtmlTemplate(sb.ToString());
        }

        public EditGameViewModel Model { get; set; }
    }
}