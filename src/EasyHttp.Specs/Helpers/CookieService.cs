using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace EasyHttp.Specs.Helpers
{
    internal class CookieService : RestServiceBase<CookieInfo>
    {
        public override object OnGet(CookieInfo request)
        {
            if (!this.Request.Cookies.ContainsKey(request.Name))
            {
                return new HttpResult
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new CookieInfo()
            {
                Name = request.Name, Value = this.Request.Cookies[request.Name].Value
            };
        }

        public override object OnPut(CookieInfo request)
        {
            this.Response.Cookies.AddCookie(new Cookie(request.Name, request.Value));

            return new HttpResult()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}