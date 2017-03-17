using System.Web.Mvc;

namespace CarDealerApp.Controllers
{
    using CarDealer.Data;
    using CarDealer.Data.Repositories;
    using CarDealer.Models;
    using CarDealer.Models.BindingModels;
    using CarDealer.Models.Enums;
    using CarDealer.Models.ViewModels.Cars;
    using CarDealer.Models.ViewModels.Parts;
    using CarDealer.Services;
    using Contracts;
    using Filters;

    [RoutePrefix("cars")]
    public class CarsController : BaseController
    {
        private readonly CarsService carsService;
        private readonly PartsService partsService;

        public CarsController()
        {
            this.partsService = new PartsService(new EfGenericRepository<Part>(Data.Context()));
            this.carsService = new CarsService(new EfGenericRepository<Car>(Data.Context()), this.partsService);
        }

        // GET: Cars
        [Route("all")]
        public ActionResult All()
        {
            var cars = this.carsService.GetAllCarsVm();

            return this.View(cars);
        }

        public ActionResult Make(string make)
        {
            var carVms = this.carsService.GetCarsByMake(make);

            return this.View("All", carVms);
        }

        public ActionResult Parts(int id)
        {
            var carsWithParts = this.carsService.GetPartsByCar(id);

            return this.View(carsWithParts);
        }

        [Route("addparttocar")]
        public ActionResult AddPartToCar(int carId)
        {
            var addPartVm = new AddPartViewModel() { CarId = carId, Quantity = 1 };
            return this.View(addPartVm);
        }

        [HttpPost]
        [Route("addparttocar")]
        public ActionResult AddPartToCar([Bind(Include = "CarId, Name,Price,Supplier,Quantity,IsImporter")] AddPartViewModel part)
        {
            this.carsService.AddPartToCar(part);

            return this.RedirectToAction("Parts", "Cars", new { id = part.CarId });
        }

        [HttpPost]
        [Route("DeletePart")]
        public ActionResult DeletePart(int partId, int carId)
        {
            this.partsService.DeletePartById(partId);
            return this.RedirectToAction("Parts", "Cars", new { id = carId });
        }

        [Route("EditPart")]
        public ActionResult EditPart(int partId, int carId)
        {
            var carPartVm = this.partsService.GetCarPartsVm(partId, carId);
            return this.View(carPartVm);
        }

        [HttpPost]
        [Route("EditPart")]
        public ActionResult EditPart(EditCarPartBindingModel editPartBm)
        {
            if (ModelState.IsValid)
            {
                this.partsService.EditPart(editPartBm);
            }

            return this.RedirectToAction("Parts", "Cars", new { id = editPartBm.CarId });
        }

        [Route("Add")]
        [Log]
        public ActionResult Add()
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var getAllPartsItems = this.partsService.GetAllParts();
            var newCarWithAllParts = new NewCarAndAllPartsViewModel() { Parts = getAllPartsItems };
            return this.View(newCarWithAllParts);
        }

        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NewCarAndAllPartsViewModel car)
        {
            if (ModelState.IsValid)
            {
                this.carsService.AddNewCar(car);
            }
            return this.RedirectToAction("All");
        }
    }
}