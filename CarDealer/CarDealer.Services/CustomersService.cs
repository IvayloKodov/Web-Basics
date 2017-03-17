namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Repositories;
    using Models;
    using Models.ViewModels;
    using Models.ViewModels.Sales;

    public class CustomersService
    {
        private readonly IRepository<Customer> customers;

        public CustomersService(IRepository<Customer> customers)
        {
            this.customers = customers;
        }

        public IEnumerable<CustomerViewModel> Customers(string order)
        {
            if (order.ToLower() == "ascending")
            {
                return this.UnOrderedCustomers()
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => !c.IsYoungDriver);
            }

            return this.UnOrderedCustomers()
                    .OrderByDescending(c => c.BirthDate)
                    .ThenBy(c => !c.IsYoungDriver);
        }

        private IQueryable<CustomerViewModel> UnOrderedCustomers()
        {
            return this.customers
                .All()
                .Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver,
                    Name = c.Name
                });
        }

        public TotalSaleCustomerViewModel GetCustomerTotalSales(int customerId)
        {
            var customerSales = this.customers
                                        .GetById(customerId)
                                        .Sales;

            var totalSalesCustomerVm = new TotalSaleCustomerViewModel()
            {
                Name = customerSales.First().Customer.Name,
                BougthCarsCount = customerSales.Count,
                TotalSpentMoney = customerSales.Sum(s => s.Car
                                                            .Parts
                                                            .Where(p => p.Price != null)
                                                            .Sum(p => (int)p.Price))
            };

            return totalSalesCustomerVm;
        }

        public void AddCustomer(Customer customer)
        {
            customer.IsYoungDriver = (customer.BirthDate.Year - DateTime.Now.Year) > 18;
            this.customers.Add(customer);
            this.customers.SaveChanges();
        }

        public CustomerViewModel GetCustomerVmById(int id)
        {
            var customer = this.customers.GetById(id);
            if (customer == null)
            {
                return null;
            }

            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsYoungDriver = customer.IsYoungDriver
            };
        }

        public void EditCustomer(Customer customer)
        {
            var customerDb = this.customers.GetById(customer.Id);

            customerDb.Name = customer.Name;
            customerDb.BirthDate = customer.BirthDate;

            this.customers.SaveChanges();
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return this.customers.All().OrderBy(c => c.Name);
        }

        public Customer GetCustomerById(int saleDetailsCustomerId)
        {
            return this.customers.GetById(saleDetailsCustomerId);
        }
    }
}