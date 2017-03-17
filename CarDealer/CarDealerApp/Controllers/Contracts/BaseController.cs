namespace CarDealerApp.Controllers.Contracts
{
    using System;
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Data.Repositories;
    using CarDealer.Models;
    using CarDealer.Models.Enums;
    using CarDealer.Services;

    public abstract class BaseController : Controller
    {
        private readonly LogsService logsService;

        protected BaseController()
        {
            this.logsService = new LogsService(new EfGenericRepository<Log>(Data.Context()));
        }

        protected void Log(string user, OperationType type, string table)
        {
            var log = new Log()
            {
                UserId = UserInfo.UserId,
                ModifiedTable = table,
                Operation = type,
                Time = DateTime.Now
            };

            this.logsService.Log(log);
        }
    }
}