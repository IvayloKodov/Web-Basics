namespace SoftUniStore.Services
{
    using System.Linq;
    using Client.Api.CommonModels.ViewModels.Games;
    using Contracts;
    using Data.Common.Repositories;
    using Data.Models;

    public class GamesService : IGamesService
    {
        private readonly IRepository<Game> games;
        private readonly IRepository<User> users;

        public GamesService(IRepository<Game> games, IRepository<User> users)
        {
            this.games = games;
            this.users = users;
        }

        public AllHomeGamesViewModel GetAllGamesVms()
        {
            var allGamesVm = this.games
                .All()
                .Select(g => new HomeGameViewModel()
                {
                    GameId = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    Size = g.Size,
                    Description = g.Description,
                    Thumbnail = g.ImageThumbnail,
                });

            return new AllHomeGamesViewModel { Games = allGamesVm };
        }

        public AllHomeGamesViewModel GetOwnGames(int currentUserId)
        {
            var ownedGamesVm = this.users
                .GetById(currentUserId)
                .Games
                .Select(g => new HomeGameViewModel()
                {
                    GameId = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    Size = g.Size,
                    Description = g.Description,
                    Thumbnail = g.ImageThumbnail,
                });

            return new AllHomeGamesViewModel { Games = ownedGamesVm };
        }

        public GameDetailsViewModel GetGameDetails(int gameId)
        {
            var gameDb = this.games.GetById(gameId);

            return new GameDetailsViewModel()
            {
                GameId = gameDb.Id,
                Size = gameDb.Size,
                Price = gameDb.Price,
                Description = gameDb.Description,
                Title = gameDb.Title,
                YouTubeId = gameDb.Trailer,
                ReleaseDate = gameDb.ReleaseDate
            };
        }

        public void BuyGame(int gameId, int buyerId)
        {
            var buyer = this.users.GetById(buyerId);

            var game = this.games.GetById(gameId);

            buyer.Games.Add(game);
            this.users.SaveChanges();
        }
    }
}