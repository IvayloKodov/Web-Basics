namespace Home
{
    using PizzaMore.Models;
    using PizzaMore.Utility;
    using System.Collections.Generic;

    public class Home
    {
        public static IDictionary<string, string> RequestParameters { get; set; }
        public static Session Session { get; set; }
        public static Header Header = new Header();
        public static string Language { get; set; }

        public static void AddDefaultLanguageCookie()
        {
            var cookies = WebUtils.GetCookies();
            bool langCookie = cookies.ContainsKey("lang");

            if (!langCookie)
            {
                Header.AddCookie(new Cookie("lang", "EN"));
                Language = "EN";
                return;
            }

            Header.AddCookie(new Cookie("lang", cookies["lang"].Value));
        }
    }
}