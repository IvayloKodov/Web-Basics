namespace SharpStore.Contracts
{
    public interface IHtmlProvider
    {
        IHtml GetHtmlPage(string pageName);
    }
}