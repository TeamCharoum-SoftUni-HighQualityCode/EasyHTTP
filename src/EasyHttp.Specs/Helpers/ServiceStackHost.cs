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
}