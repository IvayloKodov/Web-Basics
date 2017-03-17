namespace CarDealer.Models.ViewModels
{
    using System;
    using System.ComponentModel;
    using Enums;

    public class LogsViewModel
    {
        public int Id { get; set; }

        public string User { get; set; }

        public OperationType Operation { get; set; }

        [DisplayName("Modified table")]
        public string ModifiedTable { get; set; }

        public DateTime Time { get; set; }
    }
}
