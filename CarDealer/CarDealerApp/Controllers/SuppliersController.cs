using System.Web.Mvc;

namespace CarDealerApp.Controllers
{
    using System.ComponentModel;
    using CarDealer.Data;
    using CarDealer.Data.Repositories;
    using CarDealer.Models;
    using CarDealer.Models.BindingModels;
    using CarDealer.Models.Enums;
    using CarDealer.Services;
    using Contracts;
    using Filters;

    public class SuppliersController : BaseController
    {
        private readonly SuppliersService suppliersService;

        public SuppliersController()
        {
            this.suppliersService = new SuppliersService(new EfGenericRepository<Supplier>(Data.Context()));
        }

        // GET: Suppliers
        public ActionResult Index(string type)
        {
            var suppliers = this.suppliersService.GetSuppliersVms(type);
            return this.View(suppliers);
        }

        public ActionResult Add()
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View();
        }

        [HttpPost]
        [Log]
        public ActionResult Add(AddSupplierBindingModel supplierBm)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.suppliersService.AddNewSupplier(supplierBm);

            return this.RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var editSupplierVm = this.suppliersService.SupplierDetailsVm(id);

            return this.View(editSupplierVm);
        }

        [HttpPost]
        [Log]
        public ActionResult Edit(EditSupplierBindingModel editSupplier)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.suppliersService.EditSupplier(editSupplier);

            return this.RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View(id);
        }

        [HttpPost]
        [Log]
        public ActionResult Delete(string id)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.Log(UserInfo.Username, OperationType.Delete, "Supplier");

            this.suppliersService.DeleteSupplierById(int.Parse(id));

            return this.RedirectToAction("Index");
        }
    }
}