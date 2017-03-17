namespace CarDealerApp.Infrastructure.Extensions
{
    using System.Web;
    using System.Web.Mvc;
    using HtmlTags;

    public static class HtmlHelpersExtensions
    {
        public static HtmlString Submit(this HtmlHelper helper, string text,string classAttr)
        {
            var tag = new HtmlTag("input");
            tag.Attr("type", "submit")
                .Attr("class", classAttr)
                .Attr("value",text);

            return new HtmlString(tag.ToHtmlString());
        }
    }
}