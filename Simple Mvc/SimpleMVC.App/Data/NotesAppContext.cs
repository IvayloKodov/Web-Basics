namespace SimpleMVC.App.Data
{
    using System.Data.Entity;
    using Models;

    public class NotesAppContext : DbContext
    {

        public NotesAppContext()
            : base("name=NotesAppContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }
    }

}