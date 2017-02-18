namespace SimpleMVC.App.MVC.Routers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Attributes.Methods;
    using Controllers;
    using Interfaces;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;

    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParams;

        public HttpResponse Handle(HttpRequest request)
        {
            //Retrieve get parameters
            this.getParams = this.GetParams(WebUtility.UrlDecode(request.Url));

            //Retrieve post parameters
            this.postParams = this.GetPostParams(WebUtility.UrlDecode(request.Content));

            //Retrieve the request method
            this.requestMethod = request.Method.ToString();

            string[] urlParams = this.GetUrlParams(request.Url);
            //Retrieve the controller name
            this.controllerName = char.ToUpper(urlParams[0][0]) + urlParams[0].Substring(1)+ "Controller";

            //Retrieve the action name
            this.actionName = char.ToUpper(urlParams[1][0]) + urlParams[1].Substring(1);

            //Retrieve the method that should be executed
            MethodInfo method = this.GetMethod();
            if (method == null)
            {
                throw new NotSupportedException("No such method");
            }

            //Convert passed parameters to its appropriate type
            this.PrepareMethodParameters(method);

            HttpResponse response = this.CreateHttpResponse();

            return response;
        }

        private HttpResponse CreateHttpResponse()
        {
            var method = this.GetMethod();
            IInvocable actionResult = (IInvocable)this.GetMethod()
                                            .Invoke(this.GetController(), this.methodParams);

            string content = actionResult.Invoke();
            var response = new HttpResponse()
            {
                StatusCode = ResponseStatusCode.Ok,
                ContentAsUTF8 = content
            };

            return response;
        }

        private void PrepareMethodParameters(MethodInfo method)
        {
            IEnumerable<ParameterInfo> parameters = method.GetParameters();

            this.methodParams = new object[parameters.Count()]; ;


            int index = 0;
            foreach (var param in parameters)
            {
                if (param.ParameterType.IsPrimitive)
                {
                    string value = this.getParams[param.Name];
                    this.methodParams[index++] = Convert.ChangeType(value, param.ParameterType);
                }
                else
                {
                    Type bindingModelType = param.ParameterType;
                    object bindingModel = Activator.CreateInstance(bindingModelType);

                    IEnumerable<PropertyInfo> properties = bindingModelType.GetProperties();

                    foreach (var propertyInfo in properties)
                    {
                        propertyInfo.SetValue(
                            bindingModel,
                            Convert.ChangeType(
                                this.postParams[propertyInfo.Name],
                                propertyInfo.PropertyType));
                        //Here is possible no match because of capital propertyInfo.Name
                    }

                    this.methodParams[index++] = Convert.ChangeType(bindingModel, bindingModelType);
                }
            }
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;

            foreach (var methodInfo in this.GetSuitableMethods())
            {
                IEnumerable<Attribute> attributes = methodInfo
                                .GetCustomAttributes(typeof(HttpMethodAttribute))
                                .Where(a => a is HttpMethodAttribute);

                if (!attributes.Any())
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.Isvalid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            return this.GetController()
                        .GetType()
                        .GetMethods()
                        .Where(m => m.Name == this.actionName);
        }

        private Controller GetController()
        {
            var controllerType = string.Format("{0}.{1}.{2}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ControllersFolder,
                this.controllerName);

            var controller = (Controller)Activator.CreateInstance(Type.GetType(controllerType));

            return controller;
        }

        private string[] GetUrlParams(string url)
        {
            url = WebUtility.UrlDecode(url).Split('?')[0];

            string[] urlParams = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (urlParams.Length < 2)
            {
                throw new InvalidOperationException("Invalid url!");
            }

            return urlParams;
        }


        private IDictionary<string, string> GetParams(string url)
        {
            var paramStrings = url.Split('?');
            if (paramStrings.Length < 2)
            {
                return new Dictionary<string, string>();
            }

            return this.ParseParams(paramStrings[1]);
        }

        private IDictionary<string, string> GetPostParams(string content)
        {
            var paramString = content;
            if (string.IsNullOrEmpty(paramString))
            {
                return new Dictionary<string, string>();
            }

            return this.ParseParams(paramString);
        }

        private Dictionary<string, string> ParseParams(string paramString)
        {
            var parameters = new Dictionary<string, string>();
            var kvpairs = paramString.Split('&');
            foreach (var kvpair in kvpairs)
            {
                var kvpArgs = kvpair.Split('=');

                parameters[kvpArgs[0]] = kvpArgs[1];
            }

            return parameters;
        }
    }
}