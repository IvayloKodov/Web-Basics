﻿namespace CarDealer.Models.BindingModels
{
    public class AddSaleBindingModel
    {
        public int CustomerId { get; set; }

        public int CarId { get; set; }

        public double Discount { get; set; }
    }
}