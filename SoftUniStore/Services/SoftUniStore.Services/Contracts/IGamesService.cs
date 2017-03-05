namespace SoftUniStore.Services.Contracts
{
    using Client.Api.CommonModels.ViewModels.Games;

    public interface IGamesService
    {
        AllHomeGamesViewModel GetAllGamesVms();

        AllHomeGamesViewModel GetOwnGames(int currentUserId);

        GameDetailsViewModel GetGameDetails(int gameId);

        void BuyGame(int gameId, int buyerId);
    }
}