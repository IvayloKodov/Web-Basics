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

    public class CatalogController : BaseController
    {
        private readonly ICatalogService catalogService;

        public CatalogController()
        {
            this.catalogService = NinjectProvider.Kernel().Get<ICatalogService>();
        }

        [HttpGet]
        public IActionResult<IEnumerable<AdminGameViewModel>> AllGames(HttpResponse responce, HttpSession session)
        {
            if (this.RedirectNotLoggedAdmin(responce, session))
            {
                return null;
            }

            var adminGamesVm = this.catalogService.GetAllGames();

            return this.View(adminGamesVm);
        }

        [HttpGet]
        public IActionResult AddGame(HttpResponse responce, HttpSession session)
        {
            if (this.RedirectNotLoggedAdmin(responce, session))
            {
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public void AddGame(HttpResponse responce, HttpSession session, NewGameBindingModel newGameBm)
        {
            if (this.RedirectNotLoggedAdmin(responce, session))
            {
                return;
            }

            var location = this.catalogService.AddNewGame(newGameBm);

            this.Redirect(responce, location);
        }

        [HttpGet]
        public IActionResult<EditGameViewModel> EditGame(HttpResponse responce, HttpSession session,int gameId)
        {
            if (this.RedirectNotLoggedAdmin(responce, session))
            {
                return null;
            }

            var game = this.catalogService.GetGameById(gameId);

            return this.View(game);
        }

        [HttpPost] 
        public void EditGame(HttpResponse responce, HttpSession session, NewGameBindingModel editGameBm)
        {
            if (this.RedirectNotLoggedAdmin(responce, session))
            {
                return;
            }

            var location = this.catalogService.EditGame(editGameBm);

            this.Redirect(responce, location);
        }


        [HttpGet]
        public void DeleteGame(HttpResponse responce, HttpSession session, int gameId)
        {
            if (this.RedirectNotLoggedAdmin(responce, session))
            {
                return;
            }

            this.catalogService.DeleteGame(gameId);

            this.Redirect(responce,"/catalog/AllGames");
        }


        private bool RedirectNotLoggedAdmin(HttpResponse responce, HttpSession session)
        {
            if (!this.authorization.IsAuthenticatedUser(session))
            {
                this.Redirect(responce, "/store/login");
                return true;
            }

            if (!this.authorization.GetCurrentUser(session).IsAdministrator)
            {
                this.Redirect(responce, "/home/all");
                return true;
            }

            return false;
        }
    }
}