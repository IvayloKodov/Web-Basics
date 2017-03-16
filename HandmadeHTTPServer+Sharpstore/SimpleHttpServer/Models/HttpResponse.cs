namespace SimpleHttpServer.Models
{
    using System;
    using Enums;
    using System.Text;

    public class HttpResponse
    {
        public HttpResponse()
        {
            this.Content = new byte[] { };
            this.Header = new Header(HeadeType.HttpResponce);
        }

        public ResponseStatusCode StatusCode { get; set; }

        public string StatusMessage => Enum.GetName(typeof(ResponseStatusCode), this.StatusCode);

        public Header Header { get; set; }

        public byte[] Content { get; set; }

        public string ContentAsUtf8
        {
            set
            {
                this.Content = Encoding.UTF8.GetBytes(value);
            }
        }

        public override string ToString()
        {
            return $"HTTP/1.0 {this.StatusCode} {this.StatusMessage}\r\n{this.Header}";
        }
    }
}