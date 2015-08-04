namespace Http.Client
{
    using EasyHttp.Http;

    class Demo
    {
        static void Main()
        {
            HttpClient client = new HttpClient();
            var result = client.Get("https://github.com/temeliev");
            var head = client.Head("https://github.com/temeliev");
            var newTest = client.Get("http://www.winner.bg/", new { Name = "bulgarian-football", Id = "read107802" });
            var tree = result.DynamicBody;
        }
    }
}
