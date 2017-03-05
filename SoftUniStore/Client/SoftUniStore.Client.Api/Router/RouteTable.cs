namespace SoftUniStore.Client.Api.Router
{
    using System.Collections.Generic;
    using System.IO;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;
    using SimpleMVC.Routers;

    public class RouteTable
    {
        public static IEnumerable<Route> Routes => new[]
        {
             new Route
                    {
                        Name = "Bootstrap Map",
                        Method = RequestMethod.GET,
                        UrlRegex = "/bootstrap/css/bootstrap.min.css.map$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse()
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css.map")
                            };
                            response.Header.ContentType = "text/css";
                            return response;
                        }
                    },
                    new Route
                    {
                        Name = "jQuery JS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/jquery/jquery-3.1.1.js$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/jquery/jquery-3.1.1.js"),
                                Header = {ContentType = "application/x-javascript"}
                            };
                            return response;
                        }
                    },
                    new Route
                    {
                        Name = "Bootstrap JS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/bootstrap/js/bootstrap.min.js$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/js/bootstrap.min.js"),
                                Header = {ContentType = "application/x-javascript"}
                            };
                            return response;
                        }
                    },
                    new Route
                    {
                        Name = "Bootstrap CSS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/bootstrap/css/bootstrap.min.css$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css"),
                                Header = {ContentType = "text/css"}
                            };
                            return response;
                        }
                    },
                    new Route
                    {
                        Name = "All Custom CSS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/css/(.+).css$",
                        Callable = (request) =>
                        {
                            int lastSlashIndex = request.Url.LastIndexOf('/') + 1;
                            var response = new HttpResponse
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 =
                                    File.ReadAllText($"../../content/css/{request.Url.Substring(lastSlashIndex)}"),
                                Header = {ContentType = "text/css"}
                            };
                            return response;
                        }
                    },
                    new Route
                    {
                        Name = "All JPEG Images",
                        Method = RequestMethod.GET,
                        UrlRegex = @"images/(.+)\.jpg$",
                        Callable = (request) =>
                        {
                            int lastSlashIndex = request.Url.LastIndexOf('/') + 1;
                            var response = new HttpResponse
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                Content =
                                    File.ReadAllBytes($"../../content/images/{request.Url.Substring(lastSlashIndex)}"),
                                Header = {ContentType = "image/*"}
                            };
                            return response;
                        }
                    },
                    new Route
                    {
                        Name = "Controller/Action/GET",
                        Method = RequestMethod.GET,
                        UrlRegex = @"^/(.+)/(.+)$",
                        Callable = new ControllerRouter().Handle
                    },
                    new Route
                    {
                        Name = "Controller/Action/POST",
                        Method = RequestMethod.POST,
                        UrlRegex = @"^/(.+)/(.+)$",
                        Callable = new ControllerRouter().Handle
                    }
        };
    }
}