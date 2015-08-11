using System;
using System.Net;
using EasyHttp.Http;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject(typeof (HttpClient))]
    public class WhenMakingARequestThatContainsAResponseWithErrorInformation  
    {
        private static HttpClient client;
        private static HttpResponse response;

        Establish context = () =>
        {
            client = new HttpClient("https://cloudapi.ritterim.com");
            client.Request.AddExtraHeader("X-Requested-With", "XMLHttpRequest");
            client.Request.AddExtraHeader("Token-Authorization", "badpassword");
            client.Request.Accept = HttpContentTypes.ApplicationJson;
            client.Request.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(1).TotalMilliseconds);
        };

        Because of = () =>
        {
            response = client.Post("Users", new
            {
                Password = "foo",
                PasswordExpiration = DateTime.MinValue,
                Email = "new@mail.com",
                FirstName = "test",
                LastName = "fail",
                Information = new { IpAddress = "1.2.3.4" }
            }, HttpContentTypes.ApplicationJson);
        };

        It shouldReturnAllResponseInformation = () =>
        {
            response.StatusCode.ShouldEqual(HttpStatusCode.Forbidden);
            response.StatusDescription.ShouldNotBeNull();
            response.RawHeaders.Keys.Count.ShouldBeGreaterThan(0);
            response.RawHeaders["X-AspNetMvc-Version"].ShouldEqual("3.0");
            response.CacheControl.ToString().ShouldEqual("NoCache");
            response.Server.ShouldEqual("Microsoft-IIS/8.0");
        };     
    }
}