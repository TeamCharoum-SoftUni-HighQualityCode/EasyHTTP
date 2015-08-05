using System.Dynamic;
using EasyHttp.Http;
using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithOneParameterUsingExpandoObject
    {
        static ObjectToUrlSegments objectToUrlSegments;
        private static dynamic parameters;
        static string url;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
            parameters = new ExpandoObject();
            parameters.Name = "test";
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(parameters);
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("/test");       
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithTwoParametersUsingExpandoObject
    {
        static ObjectToUrlSegments objectToUrlSegments;
        private static dynamic parameters;
        static string url;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
            parameters = new ExpandoObject();
            parameters.Name = "test";
            parameters.Id = 1;
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(parameters);
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("/test/1");
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithOneParameterUsingAnonymousObject
    {
        static ObjectToUrlSegments objectToUrlSegments;
        static string url;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(new {Name = "test"});
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldEqual("/test");      
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithTwoParametersUsingAnonymousObject
    {
        static ObjectToUrlSegments objectToUrlSegments;
        static string url;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(new { Name = "test", Id=1 });
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldEqual("/test/1");      
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsItShouldEncodeValue
    {
        static ObjectToUrlSegments objectToUrlSegments;
        static string url;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(new { Name = "test<>&;"});
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldEqual("/test%3c%3e%26%3b");      
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsItShouldBeEmptyWhenPassingNull
    {
        static ObjectToUrlSegments objectToUrlSegments;
        static string url;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(null);
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldBeEmpty();      
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithOneParameterUsingStaticObject
    {
        static ObjectToUrlSegments objectToUrlSegments;
        static string url;
        static StaticObjectWithName parameter;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
            parameter = new StaticObjectWithName() {Name="test"};
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(parameter);
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldEqual("/test");        
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithTwoParametersUsingStaticObject
    {
        static ObjectToUrlSegments objectToUrlSegments;
        static string url;
        static StaticObjectWithNameAndId parameter;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
            parameter = new StaticObjectWithNameAndId() { Name = "test", Id=1 };
        };

        Because of = () => url = objectToUrlSegments.ParametersToUrl(parameter);
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldEqual("/test/1");      
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithTwoParametersUsingStaticObjectInDifferentOrder
    {
        static ObjectToUrlSegments objectToUrlSegments;
        static string url;
        static StaticObjectWithNameAndIdInDifferentOrder parameter;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
            parameter = new StaticObjectWithNameAndIdInDifferentOrder() { Name = "test", Id = 1 };
        };
        Because of = () => url = objectToUrlSegments.ParametersToUrl(parameter);
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldEqual("/1/test");     
    }

    public class StaticObjectWithNameAndIdInDifferentOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
