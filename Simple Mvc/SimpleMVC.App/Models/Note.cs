﻿namespace SimpleMVC.App.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }
    }
}