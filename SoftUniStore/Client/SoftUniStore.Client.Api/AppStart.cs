namespace SoftUniStore.Client.Api
{
    using Common.Providers;
    using Router;
    using SimpleHttpServer;
    using SimpleMVC;

    public class AppStart
    {
        public static void Main()
        {
            HtmlsProvider.Initialize();

            HttpServer server = new HttpServer(8081, RouteTable.Routes);
            MvcEngine.Run(server, "SoftUniStore.Client.Api");
        }
    }
}
