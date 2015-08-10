namespace EasyHttp.Specs.BugRepros
{
    /// <summary>
    /// A class that hold information about response data
    /// status and result
    /// </summary>
    public class Response<T>
    {
        public string Status { get; set; }
        public T Result { get; set; }
        public string[] HtmlAttributions { get; set; }
    }
}