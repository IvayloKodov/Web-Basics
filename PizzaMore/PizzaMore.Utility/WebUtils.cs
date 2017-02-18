namespace PizzaMore.Utility
{
    using System;
    using Models;
    using Contracts;
    using System.Net;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Data;

    public static class WebUtils
    {
        public static bool IsPost()
        {
            return GetRequestMethod().ToLower() == "post";
        }

        public static bool IsGet()
        {
            return GetRequestMethod().ToLower() == "get";
        }

        public static IDictionary<string, string> RetrieveGetParameters()
        {
            string getContent = WebUtility.UrlDecode(Environment.GetEnvironmentVariable(Constants.QueryString));

            if (getContent == "")
            {
                return new Dictionary<string, string>();
            }

            IDictionary<string, string> paramaters = RetrieveRequestParameters(getContent);

            return paramaters;
        }

        public static IDictionary<string, string> RetrievePostParameters()
        {
            string getContent = WebUtility.UrlDecode(Console.ReadLine());

            if (getContent == null)
            {
                return new Dictionary<string, string>();
            }


            IDictionary<string, string> paramaters = RetrieveRequestParameters(getContent);

            return paramaters;
        }


        private static IDictionary<string, string> RetrieveRequestParameters(string content)
        {
            string[] contentArgs = content.Split('&');

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var contentArg in contentArgs)
            {
                string[] parameterArg = contentArg.Split('=');
                var paramKey = parameterArg[0];
                var paramValue = parameterArg[1];

                parameters.Add(paramKey, paramValue);
            }

            return parameters;
        }

        public static ICookieCollection GetCookies()
        {
            string cookieContent = Environment.GetEnvironmentVariable(Constants.CookieGet);

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

        public static string GetRequestMethod()
        {
            return Environment.GetEnvironmentVariable(Constants.RequestMethod);
        }

        public static Session GetSession(PizzaMoreContext context)
        {
            var cookies = GetCookies();

            if (!cookies.ContainsKey(Constants.SessionIdKey))
            {
                return null;
            }

            int sessionCookieId = int.Parse(cookies[Constants.SessionIdKey].Value);

            Session session = context.Sessions.FirstOrDefault(s => s.Id == sessionCookieId);

            return session;
        }

        public static void PrintFileContent(string path)
        {
            var fileContent = File.ReadAllText(path);
            Console.WriteLine(fileContent);
        }

        public static void PageNotAllowed()
        {
            string gamePath = "../../htdocs/PizzaLab/game/index.html";
            PrintFileContent(gamePath);
        }
    }
}