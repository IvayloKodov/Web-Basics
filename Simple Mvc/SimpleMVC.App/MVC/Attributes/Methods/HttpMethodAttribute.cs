﻿namespace SimpleMVC.App.MVC.Attributes.Methods
{
    using System;

    public abstract class HttpMethodAttribute : Attribute
    {
        public abstract bool Isvalid(string requestMethod);
    }
}