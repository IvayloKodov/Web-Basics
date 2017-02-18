namespace SimpleHttpServer.Models
{
    using Enums;
    using Contracts;
    using System.Collections.Generic;
    using System.Text;

    public class Header
    {
        public Header(HeadeType type)
        {
            this.Type = type;
            this.ContentType = "text/html";
            this.Cookies = new CookieCollection();
            this.OtherParameters = new Dictionary<string, string>();
        }

        public HeadeType Type { get; set; }

        public string ContentType { get; set; }

        public string ContentLength { get; set; }

        public IDictionary<string, string> OtherParameters { get; set; }

        public ICookieCollection Cookies { get; private set; }

        public override string ToString()
        {
            var header = new StringBuilder();

            header.AppendLine($"Content-type: {this.ContentType}");

            if (this.Cookies.Count > 0)
            {
                if (this.Type == HeadeType.HttpRequest)
                {
                    header.AppendLine($"Cookie: {this.Cookies}");
                }
                else if (this.Type == HeadeType.HttpResponce)
                {
                    foreach (var cookie in this.Cookies)
                    {
                        header.AppendLine($"Set-Cookie: {cookie}");
                    }
                }
            }

            if (this.ContentLength != null)
            {
                header.AppendLine($"Content-length: {this.ContentLength}");
            }

            foreach (var other in this.OtherParameters)
            {
                header.AppendLine($"{other.Key}: {other.Value}");
            }

            header.AppendLine();

            return header.ToString();
        }
    }
}