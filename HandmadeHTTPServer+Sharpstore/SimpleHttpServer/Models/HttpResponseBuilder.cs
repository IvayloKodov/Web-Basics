namespace SimpleHttpServer.Models
{
    using System.IO;
    using Enums;

    public static class HttpResponseBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string content = File.ReadAllText("../../../SimpleHttpServer." +
                                              "/Resources/Pages/500.html");

            HttpResponse response = new HttpResponse
            {
                ContentAsUtf8 = content,
                StatusCode = ResponseStatusCode.InternalServerError
            };

            return response;
        }

        public static HttpResponse NotFound()
        {
            string content = File.ReadAllText("../../../SimpleHttpServer." +
                                              "/Resources/Pages/404.html");

            HttpResponse response = new HttpResponse
            {
                ContentAsUtf8 = content,
                StatusCode = ResponseStatusCode.NotFound
            };

            return response;
        }
    }
}