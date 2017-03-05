﻿namespace SoftUniStore.Client.Api.Views.Store
{
    using System.Text;
    using Common.Constants;
    using Common.Providers;
    using SimpleMVC.Interfaces;

    public class Register : IRenderable
    {
        public string Render()
        {
            var sb = new StringBuilder();
            sb.Append(HtmlsProvider.htmls[HtmlNames.NavNotLogged]);
            sb.Append(HtmlsProvider.htmls[HtmlNames.Register]);

            return HtmlBuilder.GetHtmlTemplate(sb.ToString());
        }
    }
}