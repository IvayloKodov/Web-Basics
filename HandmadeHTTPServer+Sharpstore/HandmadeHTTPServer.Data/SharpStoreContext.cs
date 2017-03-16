namespace HandmadeHTTPServer.Data
{
    using Contracts;
    using System.Data.Entity;
    using SimpleHttpServer.Models;

    public class SharpStoreContext : DbContext, ISharpStoreContext
    {
        public SharpStoreContext()
            : base("name=SharpStoreContext")
        {
        }

        public virtual DbSet<Knife> Knives { get; set; }

        public virtual DbSet<Message> Messages{ get; set; }
       
    }
}