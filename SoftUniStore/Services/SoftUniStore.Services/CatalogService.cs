namespace SoftUniStore.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Client.Api.Common.Validators;
    using Client.Api.CommonModels.BindingModels.Games;
    using Client.Api.CommonModels.ViewModels.Games;
    using Contracts;
    using Data.Common.Repositories;
    using Data.Models;

    public class CatalogService : ICatalogService
    {
        private readonly IRepository<Game> games;

        public CatalogService(IRepository<Game> games)
        {
            this.games = games;
        }

        public IEnumerable<AdminGameViewModel> GetAllGames()
        {
            return this.games
                .All()
                .Select(g => new AdminGameViewModel()
                {
                    GameId = g.Id,
                    Price = g.Price,
                    Size = g.Size,
                    Title = g.Title
                });
        }

        public string AddNewGame(NewGameBindingModel newGameBm)
        {
            if (!NewGameValidator.IsValidGame(newGameBm))
            {
                return "/catalog/AddGame";
            }

            var game = new Game()
            {
                Title = newGameBm.Title,
                Price = newGameBm.Price,
                Size = newGameBm.Size,
                Description = newGameBm.Description,
                ImageThumbnail = newGameBm.Thumbnail,
                Trailer = newGameBm.Trailer
            };

            this.games.Add(game);
            this.games.SaveChanges();

            return "/home/all";
        }

        public string EditGame(NewGameBindingModel editGameBm)
        {
            if (!NewGameValidator.IsValidGame(editGameBm))
            {
                return "/catalog/AllGames";
            }

            var game = this.games.GetById(editGameBm.GameId);

            game.Description = editGameBm.Description;
            game.Size = editGameBm.Size;
            game.Title = editGameBm.Title;
            game.Price = editGameBm.Price;
            game.ImageThumbnail = editGameBm.Thumbnail;
            game.Trailer = editGameBm.Trailer;

            this.games.SaveChanges();

            return "/home/all";
        }

        public EditGameViewModel GetGameById(int gameId)
        {
            var game = this.games.GetById(gameId);
            var gameVm = new EditGameViewModel()
            {
                Size = game.Size,
                Price = game.Price,
                Description = game.Description,
                Title = game.Title,
                Trailer = game.Trailer,
                ReleaseDate = game.ReleaseDate,
                Id = game.Id,
                ImageThumbnail = game.ImageThumbnail
            };

            return gameVm;
        }

        public void DeleteGame(int gameId)
        {
            this.games.Delete(gameId);
            this.games.SaveChanges();
        }
    }
}