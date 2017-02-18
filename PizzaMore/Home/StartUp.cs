namespace Home
{
    using PizzaMore.Utility;

    public class StartUp
    {
        public static void Main()
        {
            Home.AddDefaultLanguageCookie();

            if (WebUtils.IsGet())
            {
                Home.RequestParameters = WebUtils.RetrieveGetParameters();
                if (Home.RequestParameters.ContainsKey("language"))
                {
                    Home.Language = Home.RequestParameters["language"];
                }
                else
                {
                    Home.Language = Home.Header.Cookies["lang"].Value;
                }
            }
            else if (WebUtils.IsPost())
            {
                Home.RequestParameters = WebUtils.RetrievePostParameters();

                var userLangValue = Home.RequestParameters["language"];

                Home.Header.AddCookie(new Cookie("lang", userLangValue));

                Home.Language = userLangValue;
            }

            ShowPage();
        }

        public static void ShowPage()
        {
            Home.Header.Print();

            WebUtils.PrintFileContent(Home.Language == "DE" ?
                "../../htdocs/PizzaLab/home-de.html" : "../../htdocs/PizzaLab/home.html");
        }
    }
}
