namespace SharpStore.Services
{
    using System;
    using Contracts;
    using System.Linq;
    using System.Reflection;
    using SimpleHttpServer.Models;
    using HandmadeHTTPServer.Data.Contracts;

    public class ServiceProvider : ICustomServiceProvider
    {
        private readonly ISharpStoreContext context;

        public ServiceProvider(ISharpStoreContext context)
        {
            this.context = context;
        }

        public void ExecuteRequest(HttpRequest request)
        {
            this.GetService(request).Process();

            this.context.SaveChanges();
        }

        private IService GetService(HttpRequest request)
        {
            // All services are named : Service name + "Service"
            string url = request.Url;
            int lastIndexOfSlash = url.LastIndexOf('/') + 1;
            int extensionIndex = url.LastIndexOf('.');
            string serviceName = url.Substring(lastIndexOfSlash, extensionIndex - lastIndexOfSlash);

            string fullServiceName = serviceName.ToLower() + "service";

            var serviceType = Assembly
                                .GetExecutingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name.ToLower().StartsWith(fullServiceName)
                                                     && typeof(IService).IsAssignableFrom(t)
                                                     && t.IsClass
                                                     && !t.IsAbstract);

            if (serviceType == null)
            {
                return null;
            }

            object[] parameters = { this.context, request };

            var service = (IService)Activator.CreateInstance(serviceType, parameters);

            return service;
        }
    }
}