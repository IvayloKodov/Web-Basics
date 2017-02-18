namespace SharpStore
{
    using Attributes;
    using Contracts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using SimpleHttpServer.Models;
    using HandmadeHTTPServer.Data.Contracts;

    public class HtmlProvider : IHtmlProvider
    {
        private IDbSet<Knife> knives;
        private IDbSet<Message> messages;

        public HtmlProvider(ISharpStoreContext context)
        {
            this.knives = context.Knives;
            this.messages = context.Messages;
        }

        public IHtml GetHtmlPage(string pageName)
        {
            var htmlType = Assembly
                                .GetExecutingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name.ToLower().StartsWith(pageName)
                                                     && typeof(IHtml).IsAssignableFrom(t)
                                                     && t.IsClass
                                                     && !t.IsAbstract);

            if (htmlType == null)
            {
                return null;
            }

            var html = (IHtml)Activator.CreateInstance(htmlType);

            return this.InjectAttributes(html);
        }

        private IHtml InjectAttributes(IHtml html)
        {
            var fieldsOfHtmlPage = html.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            var fieldsOfHtmlManager = this.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var fieldOfHtmlPage in fieldsOfHtmlPage)
            {
                Attribute attribute = fieldOfHtmlPage.GetCustomAttribute(typeof(InjectAttribute));

                if (attribute == null) continue;

                var fieldHtmlManager = fieldsOfHtmlManager
                    .FirstOrDefault(f => f.FieldType == fieldOfHtmlPage.FieldType);
                if (fieldHtmlManager != null)
                {
                    fieldOfHtmlPage.SetValue(html, fieldHtmlManager.GetValue(this));
                }
            }

            return html;
        }
    }
}