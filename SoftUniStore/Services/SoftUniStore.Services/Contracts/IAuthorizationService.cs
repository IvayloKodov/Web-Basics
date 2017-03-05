namespace SoftUniStore.Services.Contracts
{
    using Data.Models;
    using SimpleHttpServer.Models;

    public interface IAuthorizationService
    {
        bool IsAuthenticatedUser(HttpSession session);

        User GetCurrentUser(HttpSession session);

        string Logout(HttpSession session, HttpResponse response);

        void ChangeBrowserSession(HttpResponse response);
    }
}