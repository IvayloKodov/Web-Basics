namespace SignUp
{
    using PizzaMore.Data;
    using PizzaMore.Models;
    using PizzaMore.Utility;

    public class SignUp
    {
        private static readonly Header header = new Header();

        public static void Main()
        {
            if (WebUtils.IsPost())
            {
                var parameters = WebUtils.RetrievePostParameters();
                string emailParam = parameters.ContainsKey("email") ? parameters["email"] : null;
                string passwordParam = parameters.ContainsKey("password") ? parameters["password"] : null; ;

                if (emailParam != null && passwordParam != null)
                {
                    User user = new User
                    {
                        Email = emailParam,
                        Password = PasswordHasher.Hash(passwordParam)
                    };

                    using (PizzaMoreContext context = new PizzaMoreContext())
                    {
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
            }

            ShowSignUpPage();
        }

        public static void ShowSignUpPage()
        {
            header.Print();
            WebUtils.PrintFileContent("../../htdocs/PizzaLab/signup.html");
        }
    }
}