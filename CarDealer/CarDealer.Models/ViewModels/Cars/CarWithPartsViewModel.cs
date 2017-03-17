namespace CarDealer.Models.ViewModels.Cars
{
    using System.Collections.Generic;
    using Parts;

    public class CarWithPartsViewModel
    {
        public CarWithPartsViewModel()
        {
            this.Parts = new List<PartViewModel>();
        }

        public CarViewModel Car { get; set; }

        public List<PartViewModel> Parts { get; set; }
    }
}