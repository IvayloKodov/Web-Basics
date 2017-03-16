namespace HandmadeHTTPServer.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HandmadeHTTPServer.Data.SharpStoreContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "HandmadeHTTPServer.Data.SharpStoreContext";
        }

        protected override void Seed(HandmadeHTTPServer.Data.SharpStoreContext context)
        {
            
        }
    }
}
