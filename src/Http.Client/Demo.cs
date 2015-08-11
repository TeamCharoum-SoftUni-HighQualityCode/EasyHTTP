namespace Http.Client
{
    using System;
    using System.Net;

    using EasyHttp.Http;

    using HttpClient = System.Net.Http.HttpClient;

    //using EasyHttp.Configuration;
    // using EasyHttp.Http;

    class Demo
    {
        static void Main()
        {
            //using (var httpClient = new HttpClient())
            //{
               
            //    httpClient.BaseAddress = new Uri(@"https://educloud.superhosting.bg:2083/cpsess3839927900/frontend/x3/filemanager/showfile.html?file=jsonTest.json&fileop=&dir=%2Fhome%2Ftemeliev%2Fpublic_ftp%2Fincoming&dirop=&charset=&file_charset=utf-8&baseurl=&basedir=");
            //    httpClient.DefaultRequestHeaders.Add("Accept", "text/html");
            //    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");


            //    //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            //    //...

            //    var result = httpClient.GetAsync(httpClient.BaseAddress).ContinueWith(task => Console.WriteLine(task.Result));
            //    Console.ReadKey();
            //    var teset = " ; ;; ";
            //}
             

            EasyHttp.Http.HttpClient client = new EasyHttp.Http.HttpClient();
            client.Request.Accept = HttpContentTypes.TextPlain;
            //var resultEasy = client.Get(@"https://educloud.superhosting.bg:2083/cpsess3839927900/frontend/x3/filemanager/showfile.html?file=jsonTest.json&fileop=&dir=%2Fhome%2Ftemeliev%2Fpublic_ftp%2Fincoming&dirop=&charset=&file_charset=utf-8&baseurl=&basedir="); //https://educloud.superhosting.bg:2083/cpsess3839927900/frontend/x3/filemanager/showfile.html?file=jsonTest.json&fileop=&dir=%2Fhome%2Ftemeliev%2Fpublic_ftp%2Fincoming&dirop=&charset=&file_charset=utf-8&baseurl=&basedir=
            //foreach (var t in resultEasy.DynamicBody)
            //{
            //    Console.WriteLine(t.Id);
            //    Console.WriteLine(t.Genus);
            //}
            //var head = client.Head("https://github.com/temeliev");
            var newTest = client.Get("http://www.winner.bg/", new { Name = "bulgarian-football", Id = "read107802" });
            var tree = newTest.DynamicBody;
        }
    }
}
