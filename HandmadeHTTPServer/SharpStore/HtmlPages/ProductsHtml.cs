namespace SharpStore.HtmlPages
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using Attributes;
    using Contracts;
    using SimpleHttpServer.Models;

    public class ProductsHtml : IHtml
    {
        [Inject]
        private IDbSet<Knife> knives;

        public string Print(string url)
        {
            var knives = this.knives.ToList();

            var htmlKnives = this.GenerateKnivesHtml(knives);

            string search = string.Empty;
            string[] urlParameters = url.Split('=');

            if (urlParameters.Length>1)
            {
                search = urlParameters[1];
            }

            var filteredKnives = this.knives.Where(k => k.Name.ToLower().Contains(search)).ToList();
     
            var searchedKnivesHtml = this.GenerateKnivesHtml(filteredKnives);

            return searchedKnivesHtml;
        }

        private string GenerateKnivesHtml(IEnumerable<Knife> knives)
        {
            var html = File.ReadAllText("../../Content/products.html");
            var content = string.Format(html, string.Join(Environment.NewLine, knives));

            return content;
        }
    }
}