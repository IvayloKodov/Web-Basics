namespace CarDealerApp.Filters
{
    using System;
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.Models.Enums;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (var context = new CarDealerContext())
            {
                var log = new Log()
                {
                    UserId = UserInfo.UserId,
                    ModifiedTable = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    Operation =
                        (OperationType)Enum.Parse(typeof(OperationType), filterContext.ActionDescriptor.ActionName),
                    Time = DateTime.Now
                };

                context.Logs.Add(log);
                context.SaveChanges();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}