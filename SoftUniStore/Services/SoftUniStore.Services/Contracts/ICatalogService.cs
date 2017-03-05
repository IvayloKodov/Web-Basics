namespace SoftUniStore.Services.Contracts
{
    using System.Collections.Generic;
    using Client.Api.CommonModels.BindingModels.Games;
    using Client.Api.CommonModels.ViewModels.Games;

    public interface ICatalogService
    {
        IEnumerable<AdminGameViewModel> GetAllGames();

        string AddNewGame(NewGameBindingModel newGameBm);

        string EditGame(NewGameBindingModel editGameBm);

        EditGameViewModel GetGameById(int gameId);

        void DeleteGame(int gameId);
    }
}