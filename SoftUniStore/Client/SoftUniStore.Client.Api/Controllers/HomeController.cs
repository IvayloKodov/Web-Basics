namespace SoftUniStore.Client.Api.Controllers
{
    using System.Collections.Generic;
    using CommonModels.BindingModels.Games;
    using CommonModels.ViewModels.Games;
    using Contracts;
    using DependencyContainer;
    using Ninject;
    using Services.Contracts;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Interfaces;
    using SimpleMVC.Interfaces.Generic;

    public class HomeController : BaseController
    {
        private readonly IGamesService gamesService;

        public HomeController()
        {
            this.gamesService = NinjectProvider.Kernel().Get<IGamesService>();
        }

        [HttpGet]
        public IActionResult<AllHomeGamesViewModel> All(HttpSession session, HttpResponse responce)
        {
            if (this.RedirectNotLoggedUserToLogin(responce, session))
            {
                return null;
            }

            var allGamesVms = this.gamesService.GetAllGamesVms();

            return this.View(allGamesVms);
        }

        [HttpGet]
        public IActionResult<AllHomeGamesViewModel> Owned(HttpSession session, HttpResponse responce)
        {
            if (this.RedirectNotLoggedUserToLogin(responce, session))
            {
                return null;
            }

            var currentUserId = this.authorization.GetCurrentUser(session).Id;
            var ownedGames = this.gamesService.GetOwnGames(currentUserId);

            return this.View(ownedGames, "Home", "All");
        }

        [HttpGet]
        public IActionResult<GameDetailsViewModel> GameDetails(HttpSession session, HttpResponse responce, int gameId)
        {
            if (this.RedirectNotLoggedUserToLogin(responce, session))
            {
                return null;
            }

            var gameDetailsVm = this.gamesService.GetGameDetails(gameId);

            return this.View(gameDetailsVm);
        }

        [HttpPost]
        public void BuyGame(HttpResponse responce, HttpSession session, BuyGameBindingModel game)
        {
            if (this.RedirectNotLoggedUserToLogin(responce, session))
            {
                return;
            }

            var buyerId = this.authorization.GetCurrentUser(session).Id;
            this.gamesService.BuyGame(game.GameId, buyerId);

            this.Redirect(responce, "/home/owned");
        }

        [HttpGet]
        public void Admin(HttpResponse responce, HttpSession session)
        {
            if (this.RedirectNotLoggedUserToLogin(responce, session))
            {
                return;
            }

            if (!this.authorization.GetCurrentUser(session).IsAdministrator)
            {
                this.Redirect(responce, "/home/all");
            }

            this.Redirect(responce, "/catalog/AllGames");
        }

        private bool RedirectNotLoggedUserToLogin(HttpResponse responce, HttpSession session)
        {
            if (!this.authorization.IsAuthenticatedUser(session))
            {
                this.Redirect(responce, "/store/login");
                return true;
            }

            return false;
        }
    }
}