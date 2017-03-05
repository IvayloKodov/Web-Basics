namespace SoftUniStore.Services
{
    using System.Linq;
    using Contracts;
    using Data.Common.Repositories;
    using Data.Models;
    using SimpleHttpServer.Models;
    using SimpleHttpServer.Utilities;

    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository<Login> logins;

        public AuthorizationService(IRepository<Login> logins)
        {
            this.logins = logins;
        }

        public bool IsAuthenticatedUser(HttpSession session)
        {
            if (session == null)
            {
                return false;
            }

            var sess = this.logins
                            .All()
                            .FirstOrDefault(s => s.SessionId == session.Id &&
                                                 s.IsActive);

            return sess != null;
        }

        public User GetCurrentUser(HttpSession session)
        {
            var user = this.logins
                .All()
                .Where(l => l.SessionId == session.Id)
                .Select(l => l.User)
                .FirstOrDefault();

            return user;
        }

        public string Logout(HttpSession session, HttpResponse response)
        {
            var login = this.logins
                            .All()
                            .FirstOrDefault(s => s.SessionId == session.Id
                                  && s.IsActive);

            login.IsActive = false;

            this.logins.SaveChanges();

            this.ChangeBrowserSession(response);
            
            return "/store/login";
        }

        public void ChangeBrowserSession(HttpResponse response)
        {
            var newSession = SessionCreator.Create().Id + "; HttpOnly; path=/";
            response.Header.Cookies.Add(new Cookie("sessionId", newSession));
        }
    }
}