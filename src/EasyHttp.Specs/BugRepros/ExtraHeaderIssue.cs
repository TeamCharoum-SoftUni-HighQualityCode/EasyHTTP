using EasyHttp.Http;
using Machine.Specifications;

namespace EasyHttp.Specs.BugRepros
{
    public class ExtraHeader
    {
        Establish context = () =>
            {
                http = new HttpClient();
            };
        Because of = () =>
            {
                http.Request.AddExtraHeader("X-Header", "X-Value");
            };

        It shouldAddItToTheRequest = () => { http.Request.RawHeaders.ContainsKey("X-Header"); };
        static HttpClient http;
    }
}