using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using EasyHttp.Http;
using EasyHttp.Infrastructure;
using EasyHttp.Specs.Helpers;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject(typeof (HttpClient))]
    public class WhenSendingBinaryDataAsPut
    {
        static HttpClient httpClient;
        static Guid guid;
        static HttpResponse response;
        static string rev;

        Establish context = () =>
        {
            httpClient = new HttpClient();
        };

        Because of = () =>
        {     
            var imageFile = Path.Combine("Helpers", "test.jpg");
            httpClient.PutFile(string.Format("{0}/fileupload/test.jpg", "http://localhost:16000"),imageFile,"image/jpeg");           
        };

        It shouldUploadItSuccesfully = () =>
        {
            httpClient.Response.StatusCode.ShouldEqual(HttpStatusCode.Created);
        };
    }

    [Subject(typeof (HttpClient))]
    public class WhenSendingBinaryDataAsMultipartPost
    {
        static HttpClient httpClient;
        static Guid guid;
        static HttpResponse response;
        static string rev;

        Establish context = () =>
        {
            httpClient = new HttpClient();
        };

        Because of = () =>
        {
            var imageFile = Path.Combine("Helpers", "test.jpg");       
            IDictionary<string, object> data = new Dictionary<string, object>();

            data.Add("email", "hadi@hadi.com");
            data.Add("name", "hadi");
       
            IList<FileData> files = new List<FileData>();

            files.Add(new FileData() { FieldName = "image1", ContentType = "image/jpeg", Filename = imageFile});
            files.Add(new FileData() { FieldName = "image2", ContentType = "image/jpeg", Filename = imageFile });
            httpClient.Post(string.Format("{0}/fileupload", "http://localhost:16000"), data, files);            
        };

        It shouldUploadItSuccesfully = () =>
        {
            httpClient.Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        };      
    }
}