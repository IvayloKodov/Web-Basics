namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Repositories;
    using Models;
    using Models.ViewModels;
    using Models.ViewModels.Cars;
    using Models.ViewModels.Parts;

    public class CarsService
    {
        private readonly IRepository<Car> cars;
        private readonly PartsService partsService;

        public CarsService(IRepository<Car> cars, PartsService partsService)
        {
            this.cars = cars;
            this.partsService = partsService;
        }

        public IEnumerable<CarViewModel> GetCarsByMake(string make)
        {
            return this.cars
                .All()
                .Where(c => c.Make == make)
                .Select(c => new CarViewModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance);
        }

        public CarWithPartsViewModel GetPartsByCar(int id)
        {
            var car = this.cars.GetById(id);

            var carWithParts = new CarWithPartsViewModel()
            {
                Car = new CarViewModel
                {
                    Id = car.Id,
                    Model = car.Model,
                    Make = car.Make,
                    TravelledDistance = car.TravelledDistance
                },

                Parts = car.Parts.Select(p => new PartViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            };

            return carWithParts;
        }

        public IEnumerable<CarViewModel> GetAllCarsVm()
        {
            return this.cars
                .All()
                .Select(c => new CarViewModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make);
        }

        public void AddPartToCar(AddPartViewModel addPartVm)
        {
            var car = this.cars.GetById(addPartVm.CarId);

            var part = new Part()
            {
                Name = addPartVm.Name,
                Price = addPartVm.Price,
                Quantity = addPartVm.Quantity,
                Supplier = new Supplier()
                {
                    Name = addPartVm.Supplier,
                    IsImporter = addPartVm.IsImporter
                }
            };

            car.Parts.Add(part);
            this.cars.SaveChanges();
        }

        public Car GetCarById(int carId)
        {
            return this.cars.GetById(carId);
        }

        public void AddNewCar(NewCarAndAllPartsViewModel carVm)
        {
            var newCar = new Car()
            {
                Model = carVm.Model,
                Make = carVm.Make,
                TravelledDistance = carVm.TravelledDistance
            };

            var allParts = this.partsService.GetParts(carVm.PartId);
            foreach (var part in allParts)
            {
                newCar.Parts.Add(part);
            }
            this.cars.Add(newCar);
            this.cars.SaveChanges();
        }

        public IQueryable<Car> GetAllCars()
        {
            return this.cars.All().OrderBy(c => c.Make);
        }
    }
}