using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    public class WhenBaseuriIsNullAndQueryIsNull
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(null, uri, null, false);
        It shouldReturnTheUri = () => url.ShouldEqual("uri");      
    }

    public class WhenBaseuriIsEmptyAndQueryIsNull
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseUri;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseUri = null;
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(baseUri, uri, null, false);
        It shouldReturnTheUri = () => url.ShouldEqual("uri");       
    }

    public class WhenBaseuriIsFilledAndDoesNotEndWithAForwardslashAndQueryIsNull
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseUri;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseUri = "baseuri";
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(baseUri, uri, null, false);
        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");   
    }

    public class WhenBaseuriIsFilledAndEndsWithAForwardslashAndQueryIsNull
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseUri;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseUri = "baseuri/";
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(baseUri, uri, null, false);
        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");     
    }

    public class WhenBaseuriIsFilledAndEndsWithAForwardslashAndUriStarartswithAForwardslashAndQueryIsNull
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseUri;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseUri = "baseuri/";
            uri = "/uri";
        };

        Because of = () => url = uriComposer.Compose(baseUri, uri, null, false);
        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");      
    }

    public class WhenBaseuriIsFilledAndDoesNotEndWithAForwardslashAndUriStarartswithAForwardslashAndQueryIsNull
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseUri;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseUri = "baseuri";
            uri = "/uri";
        };

        Because of = () => url = uriComposer.Compose(baseUri, uri, null, false);
        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");   
    }

    public class WhenBaseuriAndUrlAreFilledAndQueryIsNotNull
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
        static object query;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "baseuri";
            uri = "/uri";
            query = new {Name = "test"};
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, query, false);
        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri?Name=test");       
    }

    public class WhenBaseuriAndUrlAreFilledAndQueryIsNotNullAndParametersAsSegmentsIsTrue
    {
        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseUri;
        static object query;

        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseUri = "baseuri";
            uri = "/uri";
            query = new { Name = "test" };
        };

        Because of = () => url = uriComposer.Compose(baseUri, uri, query, true);
        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri/test");      
    }
}