namespace CarDealerApp.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using CarDealer.Data;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            var ctx = new CarDealerContext();
            this.ViewBag.Message = "Your application description page." + ctx.Cars.Count();

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}