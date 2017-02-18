namespace PizzaMore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        private ICollection<Pizza> suggestions;

        public User()
        {
            this.suggestions = new HashSet<Pizza>();
        }

        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Pizza> Suggestions
        {
            get { return this.suggestions; }
            set { this.suggestions = value; }
        }
    }
}
