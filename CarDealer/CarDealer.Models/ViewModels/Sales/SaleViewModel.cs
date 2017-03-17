namespace CarDealer.Models.ViewModels.Sales
{
    using Cars;

    public class SaleViewModel
    {
        public CarViewModel Car { get; set; }

        public string Customer { get; set; }

        public decimal Price { get; set; }

        public double DiscountPrice { get; set; }
    }
}