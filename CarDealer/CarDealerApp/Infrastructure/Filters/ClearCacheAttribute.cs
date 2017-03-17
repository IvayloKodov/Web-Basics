namespace CarDealerApp.Infrastructure.Filters
{
    using System.Web.Mvc;

    public class ClearCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            /*
             * OnActionExecuting - before action executing
             * onActionExecuted - after action execute 
             * OnResultExecuting - before the result execution - view is rendered
             * OnresultExecuted -..
             */

            //We can add it global to filters config
            //filters.add(new ClearCashAttribute());

            filterContext.Controller.ControllerContext.HttpContext.Cache.Remove("");
            base.OnActionExecuted(filterContext);
        }
    }
}