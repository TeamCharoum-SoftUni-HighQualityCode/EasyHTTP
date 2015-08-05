using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace EasyHttp.Specs.Helpers
{
    internal class RedirectorService : RestServiceBase<Redirect>
    {
        public override object OnGet(Redirect request)
        {
            if (this.Request.AbsoluteUri.EndsWith("redirected"))
            {
                return new HttpResult()
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new HttpResult()
            {
                StatusCode = HttpStatusCode.Redirect,
                Location = this.Request.AbsoluteUri + "/redirected"
            };
        }
    }
}