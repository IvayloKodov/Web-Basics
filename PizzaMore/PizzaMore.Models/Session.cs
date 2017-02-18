namespace PizzaMore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Session
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"{this.Id}\t{this.User.Id}";
        }
    }
}