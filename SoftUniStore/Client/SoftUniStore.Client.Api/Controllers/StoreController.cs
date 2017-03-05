namespace SoftUniStore.Client.Api.Controllers
{
    using CommonModels.BindingModels.Users;
    using Contracts;
    using DependencyContainer;
    using Ninject;
    using Services.Contracts;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Interfaces;

    public class StoreController : BaseController
    {
        private readonly IUsersService usersService;

        public StoreController()
        {
            this.usersService = NinjectProvider.Kernel().Get<IUsersService>();
        }


        [HttpGet]
        public IActionResult Login(HttpResponse responce, HttpSession session)
        {
            if (this.RedirectLoggedUserToHome(responce, session))
            {
                return null;
            }

            return this.RedirectLoggedUserToHome(responce, session) ? null : this.View();
        }

        [HttpPost]
        public void Login(LoginUserBindingModel loginModel, HttpResponse responce, HttpSession session)
        {
            if (this.RedirectLoggedUserToHome(responce, session))
            {
                return;
            }

            var location = this.usersService.LoginUser(loginModel, session, responce);

            this.Redirect(responce, location);
        }

        [HttpGet]
        public IActionResult Register(HttpResponse responce, HttpSession session)
        {
            if (this.RedirectLoggedUserToHome(responce, session))
            {
                return null;
            }

            return this.RedirectLoggedUserToHome(responce, session) ? null : this.View();
        }

        [HttpPost]
        public void Register(HttpResponse responce, HttpSession session, RegisterUserBindingModel regUserBm)
        {
            if (this.RedirectLoggedUserToHome(responce, session))
            {
                return;
            }

            var location = this.usersService.RegisterUser(regUserBm);

            this.Redirect(responce, location);
        }

        [HttpGet]
        public void Logout(HttpSession session, HttpResponse response)
        {
            var location = this.authorization.Logout(session, response);

            this.Redirect(response, location);
        }

        private bool RedirectLoggedUserToHome(HttpResponse responce, HttpSession session)
        {
            if (this.authorization.IsAuthenticatedUser(session))
            {
                this.Redirect(responce, "/home/all");
                return true;
            }
            return false;
        }
    }
}