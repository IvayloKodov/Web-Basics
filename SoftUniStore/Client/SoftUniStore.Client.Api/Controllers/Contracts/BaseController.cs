namespace SoftUniStore.Client.Api.Controllers.Contracts
{
    using DependencyContainer;
    using Ninject;
    using Services.Contracts;
    using SimpleMVC.Controllers;

    public abstract class BaseController : Controller
    {
        protected readonly IAuthorizationService authorization;

        protected BaseController()
        {
            this.authorization = NinjectProvider.Kernel().Get<IAuthorizationService>();
        }
    }
}