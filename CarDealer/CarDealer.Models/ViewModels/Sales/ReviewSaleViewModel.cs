namespace CarDealer.Models.ViewModels.Sales
{
    using System.ComponentModel;

    public class ReviewSaleViewModel
    {
        public string Customer { get; set; }

        public int CustomerId { get; set; }

        public string Car { get; set; }

        public int CarId { get; set; }

        public double Discount { get; set; }

        [DisplayName("Car Price")]
        public double CarPrice { get; set; }

        [DisplayName("Final Car Price")]
        public double FinalPrice { get; set; }
    }
}
