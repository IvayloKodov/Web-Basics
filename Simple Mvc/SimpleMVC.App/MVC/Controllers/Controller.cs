namespace SimpleMVC.App.MVC.Controllers
{
    using System.Runtime.CompilerServices;
    using Interfaces;
    using Interfaces.Generic;
    using ViewEngine;
    using ViewEngine.Generic;

    public abstract class Controller
    {
        protected Controller()
        {
            
        }

        protected IActionResult View([CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Current.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format($"{MvcContext.Current.AssemblyName}." +
                                                     $"{MvcContext.Current.ViewsFolder}." +
                                                     $"{controllerName}.") +
                                                     $"{caller}";

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult View(string controller, string action)
        {
            string fullQualifiedName = string.Format($"{MvcContext.Current.AssemblyName}." +
                                                     $"{MvcContext.Current.ViewsFolder}." +
                                                     $"{controller}." +
                                                     $"{action}");

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<T> View<T>(T model, [CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Current.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format($"{MvcContext.Current.AssemblyName}." +
                                                     $"{MvcContext.Current.ViewsFolder}." +
                                                     $"{controllerName}.") +
                                                     $"{caller}";

            return new ActionResult<T>(fullQualifiedName, model);
        }

        protected IActionResult<T> View<T>(T model, string controller, string action)
        {
            string fullQualifiedName = string.Format($"{MvcContext.Current.AssemblyName}." +
                                                     $"{MvcContext.Current.ViewsFolder}." +
                                                     $"{controller}." +
                                                     $"{action}");

            return new ActionResult<T>(fullQualifiedName, model);
        }
    }
}