namespace SoftUniStore.Data
{
    using System.Data.Entity;
    using Models;

    public class SoftUniStoreContext : DbContext, ISoftUniStoreContext
    {
        public SoftUniStoreContext()
            : base("name=SoftUniStoreContext")
        {
        }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Login> Logins { get; set; }

        public IDbSet<User> Users { get; set; }
    }
}