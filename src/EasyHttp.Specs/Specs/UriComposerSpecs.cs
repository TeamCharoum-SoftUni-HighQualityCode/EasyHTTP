using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    public class WhenBaseuriIsNullAndQueryIsNull
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(null, uri, null, false);

        It shouldReturnTheUri = () => url.ShouldEqual("uri");

        static UriComposer uriComposer;
        static string url;
        static string uri;
    }

    public class WhenBaseuriIsEmptyAndQueryIsNull
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "";
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, null, false);

        It shouldReturnTheUri = () => url.ShouldEqual("uri");

        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
    }

    public class WhenBaseuriIsFilledAndDoesNotEndWithAForwardslashAndQueryIsNull
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "baseuri";
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, null, false);

        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");

        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
    }

    public class WhenBaseuriIsFilledAndEndsWithAForwardslashAndQueryIsNull
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "baseuri/";
            uri = "uri";
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, null, false);

        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");

        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
    }

    public class WhenBaseuriIsFilledAndEndsWithAForwardslashAndUriStarartswithAForwardslashAndQueryIsNull
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "baseuri/";
            uri = "/uri";
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, null, false);

        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");

        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
    }

    public class WhenBaseuriIsFilledAndDoesNotEndWithAForwardslashAndUriStarartswithAForwardslashAndQueryIsNull
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "baseuri";
            uri = "/uri";
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, null, false);

        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri");

        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
    }

    public class WhenBaseuriAndUrlAreFilledAndQueryIsNotNull
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "baseuri";
            uri = "/uri";
            query = new {Name = "test"};
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, query, false);

        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri?Name=test");

        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
        static object query;
    }

    public class WhenBaseuriAndUrlAreFilledAndQueryIsNotNullAndParametersAsSegmentsIsTrue
    {
        Establish context = () =>
        {
            uriComposer = new UriComposer();
            baseuri = "baseuri";
            uri = "/uri";
            query = new { Name = "test" };
        };

        Because of = () => url = uriComposer.Compose(baseuri, uri, query, true);

        It shouldReturnTheBaseuriPlusUri = () => url.ShouldEqual("baseuri/uri/test");

        static UriComposer uriComposer;
        static string url;
        static string uri;
        static string baseuri;
        static object query;
    }


}