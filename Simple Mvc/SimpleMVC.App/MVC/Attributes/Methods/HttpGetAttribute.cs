namespace SimpleMVC.App.MVC.Attributes.Methods
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public override bool Isvalid(string requestMethod)
        {
            return requestMethod.ToUpper() == "GET";
        }
    }
}