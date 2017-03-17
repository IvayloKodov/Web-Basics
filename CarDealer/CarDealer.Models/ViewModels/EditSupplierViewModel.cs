namespace CarDealer.Models.ViewModels
{
    using System.Collections.Generic;
    using Parts;

    public class EditSupplierViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public IEnumerable<PartViewModel> Parts { get; set; }
    }
}
