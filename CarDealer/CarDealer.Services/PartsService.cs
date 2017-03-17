namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Repositories;
    using Models;
    using Models.BindingModels;
    using Models.ViewModels;
    using Models.ViewModels.Parts;

    public class PartsService
    {
        private readonly IRepository<Part> parts;

        public PartsService(IRepository<Part> parts)
        {
            this.parts = parts;
        }

        public void DeletePartById(int id)
        {
            var parts = this.parts.All().ToList();
            this.parts.Delete(id);
            this.parts.SaveChanges();
        }

        public void EditPart(EditCarPartBindingModel part)
        {
            var partDb = this.parts.GetById(part.PartId);
            partDb.Quantity = part.Quantity;
            partDb.Price = part.Price;
            this.parts.SaveChanges();
        }

        public EditCarPartViewModel GetCarPartsVm(int partId, int carId)
        {
            var part = this.parts.GetById(partId);

            return new EditCarPartViewModel()
            {
                Quantity = part.Quantity,
                Name = part.Name,
                Price = part.Price,
                PartId = part.Id,
                CarId = carId
            };
        }

        public List<SelectListItem> GetAllParts()
        {
            return this.parts
                .All()
                .Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();
        }

        public IEnumerable<Part> GetParts(int[] partsId)
        {
            return this.parts
                         .All()
                         .Where(p => partsId.Contains(p.Id));
        }
    }
}