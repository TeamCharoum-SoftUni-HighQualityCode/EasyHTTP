namespace EasyHttp.Specs.BugRepros
{
    public class PlaceResponse<T>
    {
        public string Status { get; set; }
        public T Result { get; set; }
        public string[] HtmlAttributions { get; set; }
    }
}