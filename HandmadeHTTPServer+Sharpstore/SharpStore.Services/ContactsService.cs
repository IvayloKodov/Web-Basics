namespace SharpStore.Services
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using Contracts;
    using HandmadeHTTPServer.Data.Contracts;
    using SimpleHttpServer.Helpers;
    using SimpleHttpServer.Models;

    public class ContactsService : IService
    {
        private readonly IDbSet<Message> messages;
        private readonly HttpRequest request;

        public ContactsService(ISharpStoreContext context, HttpRequest request)
        {
            this.messages = context.Messages;
            this.request = request;
        }

        public void Process()
        {
            Dictionary<string, string> messageArgs = WebUtils.GetRequestParams(this.request);

            Message message = new Message
            {
                Sender = messageArgs["email"],
                Subject = messageArgs["subject"],
                MessageText = messageArgs["message"]
            };

            this.messages.Add(message);
        }
    }
}