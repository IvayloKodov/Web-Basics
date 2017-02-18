namespace PizzaMore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Pizza
    {
        private ICollection<User> users;

        public Pizza()
        {
            this.users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Recipe { get; set; }

        public string ImageUrl { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }

        [ForeignKey("User")]
        public int OwnerId { get; set; }

        public virtual User User { get; set; }
    }
}