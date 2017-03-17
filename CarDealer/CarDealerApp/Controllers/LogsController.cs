using System.Web.Mvc;

namespace CarDealerApp.Controllers
{
    using CarDealer.Data;
    using CarDealer.Data.Repositories;
    using CarDealer.Models;
    using CarDealer.Services;

    public class LogsController : Controller
    {
        private readonly LogsService logsService;

        public LogsController()
        {
            this.logsService = new LogsService(new EfGenericRepository<Log>(Data.Context()));
        }

        public ActionResult All()
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var logsVm = this.logsService.GetLogsVm(null);

            return this.View(logsVm);
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
        public ActionResult ConfirmDelete(int id)
        {
            if (!UserInfo.IsLogged)
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.logsService.DeleteLogById(id);

            return this.RedirectToAction("All");
        }

        public ActionResult Search(string username)
        {
            var filteredLogs = this.logsService.GetLogsVm(username);

            return this.View("~/Views/Logs/All.cshtml", filteredLogs);
        }

        public ActionResult ClearAll()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ClearAllConfirm()
        {
            this.logsService.ClearAll();

            return this.RedirectToAction("All");
        }
    }
}