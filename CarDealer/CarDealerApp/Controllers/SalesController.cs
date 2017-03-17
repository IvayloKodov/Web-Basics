namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Data.Repositories;
    using CarDealer.Models;
    using CarDealer.Models.BindingModels;
    using CarDealer.Models.Enums;
    using CarDealer.Services;
    using Contracts;
    using Filters;

    [RoutePrefix("Sales")]
    [Route("{action=sales}")]
    public class SalesController : BaseController
    {
        private readonly SalesService saleService;
        private readonly CustomersService customersService;
        private readonly CarsService carsService;

        public SalesController()
        {
            this.saleService = new SalesService(new EfGenericRepository<Sale>(Data.Context()));
            this.customersService = new CustomersService(new EfGenericRepository<Customer>(Data.Context()));
            this.carsService = new CarsService(new EfGenericRepository<Car>(Data.Context()),
                                        new PartsService(new EfGenericRepository<Part>(Data.Context())));
        }

        // GET: Sales
        [Route("{id:int?}")]
        public ActionResult Sales(int? id)
        {
            var salesVm = this.saleService.GetSalesById(id);

            return this.View(salesVm);
        }

        [Route("{discounted}/{percent:int?}")]
        public ActionResult Discounted(int? percent)
        {
            var discountedSalesVm = this.saleService.GetDiscountedSales(percent);

            return this.View("Sales", discountedSalesVm);
        }

        public ActionResult Add()
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var customers = this.customersService.GetAllCustomers();
            var cars = this.carsService.GetAllCars();
            var addSalesVm = this.saleService.GetAddSalesVm(customers, cars);

            return this.View(addSalesVm);
        }

        [HttpPost]
        [Log]
        public ActionResult Add(AddSaleBindingModel saleDetails)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var customer = this.customersService.GetCustomerById(saleDetails.CustomerId);
            var car = this.carsService.GetCarById(saleDetails.CarId);

            var reviewSaleVm = this.saleService.GetReviewSaleVm(customer, car, saleDetails);
            return this.View("~/Views/Sales/ReviewSale.cshtml", reviewSaleVm);
        }

        [HttpPost]
        public ActionResult Finalize(AddSaleBindingModel sale)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }
            this.saleService.AddSale(sale);

            return this.RedirectToAction("Sales", "Sales");
        }
    }
}