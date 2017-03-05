namespace SoftUniStore.Client.Api.CommonModels.ViewModels.Games
{
    using System.Collections.Generic;
    using System.Text;

    public class AllHomeGamesViewModel
    {
        public IEnumerable<HomeGameViewModel> Games { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            int counter = 1;
            foreach (var game in this.Games)
            {
                sb.Append(game);
                if (counter++ % 3 == 0)
                {
                    sb.Append("</div>");
                    sb.Append("<div class=\"card-group\">");
                }
            }
            return sb.ToString();
        }
    }
}