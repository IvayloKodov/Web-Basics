namespace HandmadeHTTPServer.Data.Contracts
{
    using System.Data.Entity;
    using SimpleHttpServer.Models;

    public interface ISharpStoreContext
    {
        DbSet<Knife> Knives { get; set; }
        DbSet<Message> Messages { get; set; }
        int SaveChanges();
    }
}