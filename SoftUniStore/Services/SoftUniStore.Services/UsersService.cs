namespace SoftUniStore.Services
{
    using System.Linq;
    using Client.Api.Common.Validators;
    using Client.Api.CommonModels.BindingModels.Users;
    using Contracts;
    using Data.Common.Repositories;
    using Data.Models;
    using SimpleHttpServer.Models;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Login> logins;

        public UsersService(IRepository<User> users, IRepository<Login> logins)
        {
            this.users = users;
            this.logins = logins;
        }

        public string RegisterUser(RegisterUserBindingModel userModel)
        {
            if (this.users.All().Any(u => u.Email == userModel.Email))
            {
                return "/store/login";
            }

            if (!RegisterUserValidator.IsValid(userModel))
            {
                return "/store/register";
            }

            var newUser = new User()
            {
                FullName = userModel.FullName,
                Email = userModel.Email,
                Password = userModel.Password
            };

            if (!this.users.All().Any())
            {
                newUser.IsAdministrator = true;
            }

            this.users.Add(newUser);
            this.users.SaveChanges();

            return "/store/login";
        }

        public string LoginUser(LoginUserBindingModel loginModel, HttpSession session, HttpResponse response)
        {
            var userDb = this.users
                .All()
                .FirstOrDefault(u => u.Email == loginModel.Email &&
                                     u.Password == loginModel.Password);

            if (userDb == null)
            {
                return "/store/login";
            }

            if (this.logins.All().Any(l => l.IsActive))
            {
                var dbActiveSession = this.logins.All().First(l => l.IsActive).SessionId;
                session.Id = dbActiveSession;
                response.Header.AddCookie(new Cookie("sessionId", dbActiveSession + "; HttpOnly; path=/"));

                return "/home/all";
            }

            var login = new Login()
            {
                SessionId = session.Id,
                UserId = userDb.Id,
                IsActive = true
            };

            this.logins.Add(login);
            this.logins.SaveChanges();

            return "/home/all";
        }
    }
}