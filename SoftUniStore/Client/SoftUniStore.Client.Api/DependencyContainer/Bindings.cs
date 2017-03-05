namespace SoftUniStore.Client.Api.DependencyContainer
{
    using Data;
    using Data.Common.Repositories;
    using Ninject.Modules;
    using Services;
    using Services.Contracts;

    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISoftUniStoreContext>().To<SoftUniStoreContext>().InSingletonScope();

            this.Bind(typeof(IAuthorizationService)).To(typeof(AuthorizationService));

            this.Bind(typeof(IUsersService)).To(typeof(UsersService));

            this.Bind(typeof(IGamesService)).To(typeof(GamesService));

            this.Bind(typeof(ICatalogService)).To(typeof(CatalogService));

            this.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));
        }
    }
}