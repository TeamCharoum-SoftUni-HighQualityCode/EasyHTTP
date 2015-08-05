using System.IO;
using System.Reflection;
using EasyHttp.Http;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject(typeof(HttpClient))]
    public class WhenMakingAGetProvidedFilename
    {
        static HttpClient httpClient;
        static string filename;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "image.gif");
            File.Delete(filename);
        };

        Because of = () => httpClient.GetAsFile("http://www.jetbrains.com/img/logos/logo_jetbrains.gif",filename);
        It shouldDownloadFileToSpecifiedFilename = () => File.Exists(filename).ShouldBeTrue();        
    }
}