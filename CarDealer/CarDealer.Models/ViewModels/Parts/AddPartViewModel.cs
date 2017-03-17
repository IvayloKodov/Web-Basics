namespace CarDealer.Models.ViewModels.Parts
{
    public class AddPartViewModel
    {
        public int CarId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Supplier { get; set; }

        public bool IsImporter { get; set; }

        public double? Price { get; set; }
    }
}