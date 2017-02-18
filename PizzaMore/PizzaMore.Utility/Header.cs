namespace PizzaMore.Utility
{
    using System;
    using System.Text;
    using Contracts;

    public class Header
    {
        public Header()
        {
            this.Type = "Content-type: text/html";
            this.Cookies = new CookieCollection();
        }

        public string Type { get; set; }

        public string Location { get; set; }

        public ICookieCollection Cookies { get; set; }

        public void AddLocation(string location)
        {
            this.Location = $"Location: {location}";
        }

        public void AddCookie(Cookie cookie)
        {
            this.Cookies.AddCookie(cookie);
        }


        public void Print()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.Type);

            foreach (var cookie in this.Cookies)
            {
                sb.AppendLine($"Set-Cookie: {cookie}");
            }

            if (this.Location!=null)
            {
                sb.AppendLine(this.Location);
            }

            sb.AppendLine().AppendLine();

            return sb.ToString();
        }
    }
}