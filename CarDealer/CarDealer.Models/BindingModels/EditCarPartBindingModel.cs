namespace CarDealer.Models.BindingModels
{
    public class EditCarPartBindingModel
    {
        public int CarId { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public int PartId { get; set; }

        public int Quantity { get; set; }
    }
}