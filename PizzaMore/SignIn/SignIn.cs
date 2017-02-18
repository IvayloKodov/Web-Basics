namespace SignIn
{
    using System.Linq;
    using PizzaMore.Data;
    using PizzaMore.Models;
    using PizzaMore.Utility;

    public class SignIn
    {
        private static readonly Header header = new Header();

        public static void Main()
        {
            if (WebUtils.IsPost())
            {
                var parameters = WebUtils.RetrievePostParameters();

                string emailParam = parameters.ContainsKey("email") ? parameters["email"] : null;
                string passwordParam = parameters.ContainsKey("password") ? parameters["password"] : null; ;

                if (emailParam == null && passwordParam == null)
                {
                    return;
                }

                using (PizzaMoreContext context = new PizzaMoreContext())
                {
                    var inputHashedPass = PasswordHasher.Hash(passwordParam);

                    User user = context.Users
                        .FirstOrDefault(u => u.Email == emailParam &&
                                             u.Password == inputHashedPass);

                    if (user == null)
                    {
                        return;
                    }

                    Session session = new Session { User = user };
                    context.Sessions.Add(session);
                    context.SaveChanges();

                    var sessionDdb = context.Sessions.FirstOrDefault(s => s.UserId == user.Id);
                    header.AddCookie(new Cookie(
                        Constants.SessionIdKey, sessionDdb.Id.ToString()));
                }
            }

            ShowSignInPage();
        }


        public static void ShowSignInPage()
        {
            header.Print();
            WebUtils.PrintFileContent("../../htdocs/PizzaLab/signin.html");
        }
    }
}