namespace SimpleHttpServer.Helpers
{
    using System;
    using Models;
    using System.Collections.Generic;
    using System.Net;
    using Contracts;
    using Cookie = Models.Cookie;
    using CookieCollection = Models.CookieCollection;

    public static class WebUtils
    {
        public static Dictionary<string, string> GetRequestParams(HttpRequest request)
        {
            string requestContent = WebUtility.UrlDecode(request.Content);

            string[] parameters = requestContent.Split('&');

            Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

            foreach (var parameter in parameters)
            {
                string[] parameterInfo = parameter.Split('=');
                nameValuePairs.Add(parameterInfo[0], parameterInfo[1]);
            }

            return nameValuePairs;
        }

        public static ICookieCollection GetCookies()
        {
            string cookieContent = Environment.GetEnvironmentVariable("HTTP_COOKIE");

            if (cookieContent == "")
            {
                return new CookieCollection();
            }

            string[] cookiesArgs = cookieContent.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var cookies = new CookieCollection();

            foreach (var cookiesArg in cookiesArgs)
            {
                var cookieName = cookiesArg.Split('=')[0];

                var cookieValue = cookiesArg.Split('=')[1];

                var cookie = new Cookie(cookieName, cookieValue);

                cookies.AddCookie(cookie);
            }

            return cookies;
        }
    }
}