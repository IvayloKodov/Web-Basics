namespace SharpStore.Services.Contracts
{
    using SimpleHttpServer.Models;

    public interface ICustomServiceProvider
    {
        void ExecuteRequest(HttpRequest request);
    }
}