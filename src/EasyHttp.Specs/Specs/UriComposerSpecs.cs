using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    public class WhenBaseuriIsNullAndQueryIsNull
    {
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;

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
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;
        private static string baseUri;

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
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;
        private static string baseUri;

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
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;
        private static string baseUri;

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
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;
        private static string baseUri;

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
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;
        private static string baseUri;

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
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;
        private static string baseuri;
        private static object query;

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
        private static UriComposer uriComposer;
        private static string url;
        private static string uri;
        private static string baseUri;
        private static object query;

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