namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Repositories;
    using Models;
    using Models.BindingModels;
    using Models.ViewModels;
    using Models.ViewModels.Parts;

    public class SuppliersService
    {
        private readonly IRepository<Supplier> suppliers;

        public SuppliersService(IRepository<Supplier> suppliers)
        {
            this.suppliers = suppliers;
        }


        public IEnumerable<SupplierViewModel> GetSuppliersVms(string type)
        {
            IQueryable<Supplier> suppliers;
            if (type == null)
            {
                suppliers = this.suppliers.All();
            }
            else
            {
                suppliers = this.suppliers
                    .All()
                    .Where(s => s.IsImporter == (type == "importers" ? true : false));
            }

            return suppliers.Select(s => new SupplierViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                IsImporter = s.IsImporter
            });
        }

        public void AddNewSupplier(AddSupplierBindingModel supplierBm)
        {
            var supplier = new Supplier()
            {
                Name = supplierBm.Name,
                IsImporter = supplierBm.IsImporter
            };

            this.suppliers.Add(supplier);
            this.suppliers.SaveChanges();
        }

        public void DeleteSupplierById(int id)
        {
            var supplierForDelete = this.suppliers.GetById(id);
            this.suppliers.Delete(supplierForDelete);

            this.suppliers.SaveChanges();
        }

        public EditSupplierViewModel SupplierDetailsVm(int id)
        {
            var supplier = this.suppliers.GetById(id);

            var editSupplierVm = new EditSupplierViewModel()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                IsImporter = supplier.IsImporter,
                Parts = supplier.Parts.Select(p => new PartViewModel()
                {
                    Price = p.Price,
                    Name = p.Name,
                    Id = p.Id
                })
            };

            return editSupplierVm;
        }

        public void EditSupplier(EditSupplierBindingModel editSupplier)
        {
            var supplierDb = this.suppliers.GetById(editSupplier.Id);

            supplierDb.Name = editSupplier.Name;
            supplierDb.IsImporter = editSupplier.IsImporter;

            this.suppliers.SaveChanges();
        }
    }
}