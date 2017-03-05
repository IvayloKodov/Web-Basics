namespace SoftUniStore.Client.Api.Common.Validators
{
    using System.Text.RegularExpressions;
    using CommonModels.BindingModels.Games;

    public class NewGameValidator
    {
        public static bool IsValidGame(NewGameBindingModel game)
        {
            //Why you inject in data more than 5 symbols for title..I made it 50 for test cases!
            string titleRegex = "^[A-Z]{1}.{2,50}$";
            if (!Regex.IsMatch(game.Title, titleRegex))
            {
                return false;
            }

            if (game.Price <= 0 || game.Size <= 0)
            {
                return false;
            }

            if (!Regex.IsMatch(game.Trailer, "^.{11}$"))
            {
                return false;
            }

            if (!game.Thumbnail.StartsWith("http://") && !game.Thumbnail.StartsWith("https://"))
            {
                return false;
            }

            if (game.Description.Length < 20)
            {
                return false;
            }

            return true;
        }
    }
}