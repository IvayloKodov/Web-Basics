namespace CarDealer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    public class Log
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public OperationType Operation { get; set; }

        public string ModifiedTable { get; set; }

        public DateTime Time { get; set; }
    }
}
