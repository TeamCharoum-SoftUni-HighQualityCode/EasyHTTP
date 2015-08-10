using System.Collections.Generic;
using System.IO;
using System.Net;
using EasyHttp.Http;
using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.BugRepros
{

    public class FileUploadWasFailingBecauseFieldnameForFileFieldWasNotBeingSet
    {
        static HttpClient httpClient;
        static HttpResponse response;

        Establish context = () =>
            {
                httpClient = new HttpClient();
            };

        Because of = () =>
            {
                var fileName = Path.Combine("Helpers", "test.xml");                 
                IDictionary<string, object> data = new Dictionary<string, object>();
                data.Add("runTest", " Run Test ");
                IList<FileData> files = new List<FileData>();

                files.Add(new FileData() { FieldName = "file", ContentType = "text/xml", FileName = fileName });
                httpClient.Post("https://loandelivery.intgfanniemae.com/mismoxtt/mismoValidator.do", data, files);
                response = httpClient.Response;
            };
        It shouldSayThatOperationWasSuccessful = () => response.RawText.ShouldNotContain("Please select a file to test.");            
    }
}