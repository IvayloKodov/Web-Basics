namespace CarDealer.Models.ViewModels.Cars
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class NewCarAndAllPartsViewModel
    {
        public string Model { get; set; }

        public string Make { get; set; }

        public long TravelledDistance { get; set; }

        public int[] PartId { get; set; }

        public List<SelectListItem> Parts { get; set; }
    }
}