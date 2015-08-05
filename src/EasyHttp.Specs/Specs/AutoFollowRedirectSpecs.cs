namespace EasyHttp.Specs.Specs
{
    using System.Net;
    using EasyHttp.Http;
    using Machine.Specifications;

    public class AutoFollowRedirectSpecs
    {
        private static HttpClient httpClient;

        [Subject("HttpClient")]
        public class WhenMakingAGetRequestWithAutoRedirectOn
        {
            private Establish context = () =>
                {
                    httpClient = new HttpClient();
                };

            private Because of = () => httpClient.Get("http://localhost:16000/redirector");

            private It shouldReturnStatusCodeOfOk =
                () => httpClient.Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            private It shouldRedirect = () => httpClient.Response.Location.ShouldBeEmpty();
        }

        [Subject("HttpClient")]
        public class WhenMakingAGetRequestWithAutoRedirectOff
        {
            private Establish context = () =>
                {
                    httpClient = new HttpClient();
                };

            private Because of = () =>
            {
                httpClient.Request.AllowAutoRedirect = false;
                httpClient.Get("http://localhost:16000/redirector");
            };

            private It shouldReturnStatusCodeOfRedirect =
                () => httpClient.Response.StatusCode.ShouldEqual(HttpStatusCode.Redirect);
            private It shouldRedirect = () => httpClient.Response.Location.ShouldEndWith("redirected");
        }
    }
}