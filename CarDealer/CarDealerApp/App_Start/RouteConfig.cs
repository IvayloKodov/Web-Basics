namespace CarDealerApp
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Customers",
                "customers/all/{order}",
                defaults: new { controller = "customers", action = "all", order = "Ascending" }
                );

            routes.MapRoute(
             "Cars",
             "cars/{make}",
             defaults: new { controller = "cars", action = "make" }
             );
            
            routes.MapRoute(
             "CarsWithParts",
             "cars/{id}/parts",
             defaults: new { controller = "Cars", action = "Parts" }
             );

            routes.MapRoute(
             "TotalSalesByCustomer",
             "customers/{id}",
             defaults: new { controller = "Customers", action = "TotalSalesByCustomer", id = UrlParameter.Optional }
             );


            routes.MapRoute(
             "Default",
             "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
        }
    }
}