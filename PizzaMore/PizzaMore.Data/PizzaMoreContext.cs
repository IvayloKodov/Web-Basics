namespace PizzaMore.Data
{
    using System.Data.Entity;
    using Models;

    public class PizzaMoreContext : DbContext
    {
        public PizzaMoreContext()
            : base("name=PizzaMoreContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public virtual DbSet<Pizza> Pizzas { get; set; }
    }
}