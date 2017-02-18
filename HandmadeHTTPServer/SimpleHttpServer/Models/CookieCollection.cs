namespace SimpleHttpServer.Models
{
    using Contracts;
    using System.Collections;
    using System.Collections.Generic;
   
    public class CookieCollection : ICookieCollection
    {
        public CookieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }

        public IDictionary<string, Cookie> Cookies { get; private set; }

        public void AddCookie(Cookie cookie)
        {
            string cookieName = cookie.Name;

            if (!this.Cookies.ContainsKey(cookieName))
            {
                this.Cookies.Add(cookieName, cookie);
            }
            else
            {
                this.Cookies[cookieName] = cookie;
            }
        }

        public void RemoveCookie(string cookieName)
        {
            if (this.Cookies.ContainsKey(cookieName))
            {
                this.Cookies.Remove(cookieName);
            }
        }

        public bool ContainsKey(string key)
        {
            return this.Cookies.ContainsKey(key);
        }

        public int Count => this.Cookies.Count;

        public Cookie this[string key]
        {
            get { return this.Cookies[key]; }
            set
            {
                if (this.Cookies.ContainsKey(key))
                {
                    this.Cookies[key] = value;
                }
                else
                {
                    this.Cookies.Add(key, value);
                }
            }
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("; ", this.Cookies.Values);
        }
    }
}