namespace SimpleHttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using Enums;
    using Helpers;
    using Models;

    public class HttpProcessor
    {
        public HttpProcessor(IEnumerable<Route> routes)
        {
            this.Routes = new List<Route>(routes);
        }

        public HttpRequest Request { get; set; }

        public HttpResponse Response { get; set; }

        public IList<Route> Routes { get; set; }

        public void HandleClient(TcpClient tcpClient)
        {
            using (var stream = tcpClient.GetStream())
            {
                this.Request = this.GetRequest(stream);
                this.Response = this.RouteRequest();
                StreamUtils.WriteResponse(stream, this.Response);
            }
        }

        private HttpResponse RouteRequest()
        {
            var routes = this.Routes
                .Where(x => Regex.Match(Request.Url, x.UrlRegex).Success)
                .ToList();

            if (!routes.Any())
            {
                return HttpResponseBuilder.NotFound();
            }

            var route = routes.SingleOrDefault(x => x.Method == Request.Method);

            if (route == null)
            {
                return new HttpResponse()
                {
                    StatusCode = ResponseStatusCode.MethodNotAllowed
                };
            }

            try
            {
                return route.Callable(Request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return HttpResponseBuilder.InternalServerError();
            }
        }

        private HttpRequest GetRequest(NetworkStream stream)
        {
            var requestArgs = StreamUtils
                .ReadLine(stream)
                .Split(' ');
            string requestMethod = requestArgs[0];
            string requestUrl = requestArgs[1];
            string requestProtocolVersion = requestArgs[2];

            if (requestMethod == null || requestUrl == null || requestProtocolVersion == null)
            {
                throw new NullReferenceException("Some of request elements is missing!");
            }

            Header header = new Header(HeadeType.HttpRequest);
            string line;
            while ((line = StreamUtils.ReadLine(stream)) != null)
            {
                if (line == "")
                {
                    break;
                }

                var headerParams = line.Split(new[] { ':' }, 2).Select(s => s.Trim()).ToArray();
                string name = headerParams[0];
                string value = headerParams[1];

                if (name.ToLower() == "cookie")
                {
                    string[] cookies = value.Split(';');
                    foreach (var cookie in cookies)
                    {
                        string[] cookieArgs = cookie.Split('=');
                        var cookieName = cookieArgs[0];
                        var cookieValue = cookieArgs[1];

                        Cookie cookieObject = new Cookie { Name = cookieName, Value = cookieValue };
                        header.Cookies.AddCookie(cookieObject);
                    }
                }
                else if (name.ToLower() == "content-length")
                {
                    header.ContentLength = value;
                }
                else
                {
                    header.OtherParameters.Add(name, value);
                }
            }

            string content = null;
            if (header.ContentLength != null)
            {
                int totalBytes = Convert.ToInt32(header.ContentLength);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];
                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = stream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }

            var request = new HttpRequest
            {
                Method = (RequestMethod)Enum.Parse(typeof(RequestMethod), requestMethod),
                Url = requestUrl,
                Header = header,
                Content = content
            };

            Console.WriteLine("-REQUEST-----------------------------");
            Console.WriteLine(request);
            Console.WriteLine("------------------------------");

            return request;
        }
    }
}
