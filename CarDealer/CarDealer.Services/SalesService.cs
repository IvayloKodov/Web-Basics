namespace CarDealer.Services
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Web.Mvc;
    using Data.Repositories;
    using Models;
    using Models.BindingModels;
    using Models.ViewModels;
    using Models.ViewModels.Cars;
    using Models.ViewModels.Sales;

    public class SalesService
    {
        private readonly IRepository<Sale> sales;

        public SalesService(IRepository<Sale> sales)
        {
            this.sales = sales;
        }


        public IEnumerable<SaleViewModel> GetSalesById(int? id)
        {
            return this.FilterSales(id)
                .Select(s => new SaleViewModel
                {
                    Price = s.Car.Parts.Any() ? s.Car.Parts.Where(p => p.Price != null).Sum(p => (int)p.Price) : 0,
                    Customer = s.Customer.Name,
                    DiscountPrice = s.Car.Parts.Any() ? s.Car.Parts.Sum(p => (int)p.Price) * (1 - s.Discount) : 0,
                    Car = new CarViewModel()
                    {
                        Model = s.Car.Model,
                        Make = s.Car.Make,
                        TravelledDistance = s.Car.TravelledDistance
                    }
                });
        }


        private IQueryable<Sale> FilterSales(int? id)
        {
            if (id == null)
            {
                return this.sales.All();
            }
            return this.sales.All().Where(s => s.Id == id);
        }

        public IEnumerable<SaleViewModel> GetDiscountedSales(int? percent)
        {
            return this.FilterDiscountSales(percent)
                        .Select(s => new SaleViewModel
                        {
                            Price = s.Car.Parts.Any() ? s.Car.Parts.Sum(p => (int)p.Price) : 0,
                            Customer = s.Customer.Name,
                            DiscountPrice = s.Car.Parts.Any() ? s.Car.Parts.Sum(p => (int)p.Price) * (1 - s.Discount) : 0,
                            Car = new CarViewModel()
                            {
                                Model = s.Car.Model,
                                Make = s.Car.Make,
                                TravelledDistance = s.Car.TravelledDistance
                            }
                        });
        }

        private IQueryable<Sale> FilterDiscountSales(int? percent)
        {
            if (percent == null)
            {
                return this.sales.All().Where(s => s.Discount != 0);
            }
            return this.sales.All().Where(s => s.Discount == percent / 100d);
        }

        public IEnumerable<double> GetAllDiscounts()
        {
            return this.sales.All().Select(s => s.Discount).Distinct().OrderBy(d => d);
        }

        public AddSaleViewModel GetAddSalesVm(IQueryable<Customer> customers, IQueryable<Car> cars)
        {
            var addSalesVm = new AddSaleViewModel()
            {
                Car = cars.Select(c => new SelectListItem { Text = c.Make + " " + c.Model, Value = c.Id.ToString() }),
                Customer = customers.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                Discount = this.GetAllDiscounts().Select(d => new SelectListItem { Text = d.ToString(CultureInfo.InvariantCulture), Value = d.ToString(CultureInfo.InvariantCulture) })
            };

            return addSalesVm;
        }

        public ReviewSaleViewModel GetReviewSaleVm(Customer customer, Car car, AddSaleBindingModel saleDetails)
        {
            var carPrice = car.Parts.Where(p => p.Price != null).Sum(p => (double)p.Price);

            var reviewSaleVm = new ReviewSaleViewModel()
            {
                Customer = customer.Name,
                CustomerId = saleDetails.CustomerId,
                Car = $"{car.Make} {car.Model}",
                CarId = saleDetails.CarId,
                Discount = saleDetails.Discount,
                CarPrice = carPrice,
                FinalPrice = carPrice * (1 - saleDetails.Discount)
            };

            return reviewSaleVm;
        }

        public void AddSale(AddSaleBindingModel sale)
        {
            var newSale = new Sale()
            {
                CustomerId = sale.CustomerId,
                CarId = sale.CarId,
                Discount = sale.Discount
            };

            this.sales.Add(newSale);
            this.sales.SaveChanges();
        }
    }
}