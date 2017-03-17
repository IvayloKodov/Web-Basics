using System.Web.Mvc;

namespace CarDealerApp.Controllers
{
    using System.Net;
    using CarDealer.Data;
    using CarDealer.Data.Repositories;
    using CarDealer.Models;
    using CarDealer.Services;
    using Filters;


    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        private readonly CustomersService customerService;

        public CustomersController()
        {
            this.customerService = new CustomersService(new EfGenericRepository<Customer>(Data.Context()));
        }

        // GET: Customers
        public ActionResult All(string order)
        {
            var customersVm = this.customerService.Customers(order);
            return this.View(customersVm);
        }

        public ActionResult TotalSalesByCustomer(int id)
        {
            var totalSalesVm = this.customerService.GetCustomerTotalSales(id);
            return this.View(totalSalesVm);
        }

        // GET: Create customer
        [Route("create")]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult Create([Bind(Include = "Name,BirthDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                this.customerService.AddCustomer(customer);
                return RedirectToAction("All");
            }

            return View(customer);
        }

        [Route("edit")]
        public ActionResult Edit(int id)
        {
            var customer = this.customerService.GetCustomerVmById(id);
            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return this.View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        [Log]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate")] Customer customer)
        {
            if (this.ModelState.IsValid)
            {
                this.customerService.EditCustomer(customer);
            }

            return this.RedirectToAction("All", "Customers");
        }
    }
}