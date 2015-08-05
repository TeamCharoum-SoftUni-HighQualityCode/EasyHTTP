using EasyHttp.Http;
using EasyHttp.Specs.Helpers;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject("HttpClient Init")]
    public class WhenCreatingANewInstance
    {
        static HttpClient httpClient;
        Because of = () => { httpClient = new HttpClient(); };
        It shouldReturnNewInstanceUsingDefaultConfiguration = () => httpClient.ShouldNotBeNull();
    }

    [Subject("Initializing with base url")]
    public class WhenCreatingANewInstanceWithBaseUrl
    {
        static HttpClient httpClient;

        Because of = () =>
        {
            httpClient = new HttpClient("http://localhost:16000");
            httpClient.Get("/hello");
        };
        It shouldPrefixAllRequestsWithTheBaseUrl = () => httpClient.Request.Uri.ShouldEqual("http://localhost:16000/hello");    
    }
}