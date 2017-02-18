namespace SimpleMVC.App
{
    using MVC;
    using SimpleHttpServer;

    class AppStart
    {
        static void Main()
        {
            HttpServer server = new HttpServer(8081,RouteTable.Routes);
            MvcEngine.Run(server);
        }
    }
}