using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace EasyHttp.Specs.Helpers
{
    internal class FilesService : RestServiceBase<Files>
    {
        public override object OnPut(Files request)
        {
            if (base.Request.ContentType == "image/jpeg")
            {
                return new HttpResult()
                {
                    StatusCode = HttpStatusCode.Created
                };
            }

            return new HttpResult()
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        public override object OnPost(Files request)
        {
            if (base.Request.Files.Length == 2)
            {
                return new HttpResult()
                {
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new HttpResult()
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }
    }
}
