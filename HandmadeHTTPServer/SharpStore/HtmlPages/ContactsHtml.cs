namespace SharpStore.HtmlPages
{
    using System.IO;
    using Contracts;

    public class ContactsHtml : IHtml
    {
        public string Print(string url)
        {
            return File.ReadAllText("../../Content/contacts.html");
        }
    }
}