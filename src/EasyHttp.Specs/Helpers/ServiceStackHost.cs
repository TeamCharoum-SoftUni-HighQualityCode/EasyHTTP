using System;
using ServiceStack.WebHost.Endpoints;

namespace EasyHttp.Specs.Helpers
{
    //Define the Web Services AppHost
    internal class ServiceStackHost : AppHostHttpListenerBase
    {
        public ServiceStackHost() : base("StarterTemplate HttpListener", typeof(HelloService).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            this.Routes.Add<Hello>("/hello")
                  .Add<Hello>("/hello/{Name}");

            this.Routes.Add<Files>("/fileupload/{Name}")
                  .Add<Files>("/fileupload");

            this.Routes.Add<CookieInfo>("/cookie")
                  .Add<CookieInfo>("/cookie/{Name}");

            this.Routes.Add<Redirect>("/redirector")
                  .Add<Redirect>("/redirector/redirected");
        }
    }

    public class Hello
    {
        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.name = value;
            }
        }
    }

    public class HelloResponse
    {
        private string result;

        public string Result
        {
            get
            {
                return this.Result;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.result = value;
            }
        }
    }

    public class Redirect
    {
        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.name = value;
            }
        }
    }

    public class Files
    {
        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.name = value;
            }
        }
    }

    public class CookieInfo
    {
        private string name;
        private string value;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.name = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.value = value;
            }
        }
    }

    
}