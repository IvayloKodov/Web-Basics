namespace SharpStore
{
    using System.IO;
    using SimpleHttpServer;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;
    using HandmadeHTTPServer.Data;
    using System.Collections.Generic;
    using Contracts;
    using HandmadeHTTPServer.Data.Contracts;
    using Services;
    using Services.Contracts;
    using HttpResponse = SimpleHttpServer.Models.HttpResponse;

    public class SharpStore
    {
        public static void Main()
        {
            ISharpStoreContext context = new SharpStoreContext();
            IHtmlProvider htmlProvider = new HtmlProvider(context);
            ICustomServiceProvider serviceProvider = new ServiceProvider(context);

            var routes = new List<Route>()
            {
                new Route()
                {
                    Name = "Html pages",
                    Method = RequestMethod.GET,
                    UrlRegex = "^\\/.+\\.html.*",
                    Callable = (request) =>
                    {
                        string pageName = GetPageNameByUrl(request);
                        IHtml html = htmlProvider.GetHtmlPage(pageName);

                        var response = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUtf8 = html?.Print(request.Url)
                        };
                        return response;
                    }
                },
                 new Route()
                {
                    Name = "Html pages Post",
                    Method = RequestMethod.POST,
                    UrlRegex = "^\\/.+\\.html$",
                    Callable = (request) =>
                    {
                        string pageName = GetPageNameByUrl(request);
                        IHtml html = htmlProvider.GetHtmlPage(pageName);

                        serviceProvider.ExecuteRequest(request);

                        var response = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUtf8 =html.Print(request.Url)
                        };
                        return response;
                    }
                },
               new Route()
                {
                    Name = "Carousel CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/content/css/carousel.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUtf8 = File.ReadAllText("../../content/css/carousel.css"),
                            Header = { ContentType = "text/css" }
                        };
                        return response;
                    }
                },
               new Route()
                {
                    Name = "Products CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/content/productsTest.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUtf8 = File.ReadAllText("../../content/products.css"),
                            Header = { ContentType = "text/css" }
                        };
                        return response;
                    }
                },
                new Route()
                {
                    Name = "Bootstrap JS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/bootstrap/js/bootstrap.min.js$",
                    Callable = (request) => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUtf8 = File.ReadAllText("../../content/bootstrap/js/bootstrap.min.js"),
                        Header = { ContentType = "application/x-javascript" }
                    }
                },
                new Route()
                {
                    Name = "Bootstrap CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/bootstrap/css/bootstrap.min.css$",
                    Callable = request => new HttpResponse()
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUtf8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css"),
                        Header = { ContentType = "text/css" }
                    }
                }
            };

            HttpServer server = new HttpServer(8081, routes);
            server.Listen();
        }

        private static string GetPageNameByUrl(HttpRequest request)
        {
            string url = request.Url;

            int lastIndexOfSlash = url.LastIndexOf('/') + 1;

            int extensionIndex = url.LastIndexOf('.');

            string pageName = url.Substring(lastIndexOfSlash, extensionIndex - lastIndexOfSlash);

            return pageName.ToLower()+"html";
        }
    }
}