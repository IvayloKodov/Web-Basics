namespace CarDealer.Models.ViewModels.Sales
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class AddSaleViewModel
    {
        public IEnumerable<SelectListItem> Customer { get; set; }

        public IEnumerable<SelectListItem> Car { get; set; }

        public IEnumerable<SelectListItem> Discount { get; set; }
    }
}