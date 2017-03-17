using System.Web.Mvc;

namespace CarDealerApp.Controllers
{
    using CarDealer.Data;
    using CarDealer.Data.Repositories;
    using CarDealer.Models;
    using CarDealer.Models.BindingModels;
    using CarDealer.Services;

    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private readonly UsersService usersService;

        public UsersController()
        {
            this.usersService = new UsersService(new EfGenericRepository<User>(Data.Context()));
        }

        public ActionResult Register()
        {
            if (UserInfo.IsLogged)
            {
                return this.RedirectToAction("Index", "Home");
            }
            return this.View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUserBindingModel userBm)
        {
            if (UserInfo.IsLogged)
            {
                return this.RedirectToAction("Index", "Home");
            }
            this.usersService.RegisterUser(userBm);

            return this.RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            if (UserInfo.IsLogged)
            {
                return this.RedirectToAction("Index", "Home");
            }
            return this.View();
        }

        [HttpPost]
        public ActionResult Login(LoginUserBm loginBm)
        {
            if (UserInfo.IsLogged)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var user = this.usersService.LogInUser(loginBm);
            if (user == null)
            {
                return this.RedirectToAction("Login");
            }

            UserInfo.IsLogged = user != null;
            UserInfo.Username = loginBm.Username;
            UserInfo.UserId = user.Id;

            return this.View("~/Views/Home/Index.cshtml");
        }

        //[HttpPost]
        public ActionResult Logout()
        {
            UserInfo.IsLogged = false;
            return this.RedirectToAction("Index", "Home");
        }
    }
}