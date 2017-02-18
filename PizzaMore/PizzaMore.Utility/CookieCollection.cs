namespace PizzaMore.Utility
{
    using System.Collections;
    using Contracts;
    using System.Collections.Generic;

    public class CookieCollection : ICookieCollection
    {
        private readonly IDictionary<string, Cookie> cookies;

        public CookieCollection()
        {
            this.cookies = new Dictionary<string, Cookie>();
        }

        public void AddCookie(Cookie cookie)
        {
            string cookieName = cookie.Name;

            if (!this.cookies.ContainsKey(cookieName))
            {
                this.cookies.Add(cookieName, cookie);
            }
            else
            {
                this.cookies[cookieName] = cookie;
            }
        }

        public void RemoveCookie(string cookieName)
        {
            if (this.cookies.ContainsKey(cookieName))
            {
                this.cookies.Remove(cookieName);
            }
        }

        public bool ContainsKey(string key)
        {
            return this.cookies.ContainsKey(key);
        }

        public int Count => this.cookies.Count;

        public Cookie this[string key]
        {
            get { return this.cookies[key]; }
            set { this.cookies[key] = value; }
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}