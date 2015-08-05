using System.Dynamic;
using EasyHttp.Http;
using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersWithOneParameterUsingExpandoObject
    {
        static ObjectToUrlParameters objectToUrlParameters;
        private static dynamic parameters;
        static string url;

        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
            parameters = new ExpandoObject();
            parameters.Name = "test";
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(parameters);
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test");       
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersWithTwoParametersUsingExpandoObject
    {
        static ObjectToUrlParameters objectToUrlParameters;
        private static dynamic parameters;
        private static string url;

        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
            parameters = new ExpandoObject();
            parameters.Name = "test";
            parameters.Id = 1;
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(parameters);

        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test&Id=1");      
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersWithOneParameterUsingAnonymousObject
    {
        static ObjectToUrlParameters objectToUrlParameters;
        static string url;

        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(new {Name = "test"});

        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test");       
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersWithTwoParametersUsingAnonymousObject
    {
        static ObjectToUrlParameters objectToUrlParameters;
        static string url;

        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(new { Name = "test", Id=1 });
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test&Id=1");        
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersItShouldEncodeValue
    {
        static ObjectToUrlParameters objectToUrlParameters;
        static string url;

        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(new { Name = "test<>&;"});
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test%3c%3e%26%3b");       
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersItShouldBeEmptyWhenPassingNull
    {
        static ObjectToUrlParameters objectToUrlParameters;
        static string url;

        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(null);
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldBeEmpty();
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersWithOneParameterUsingStaticObject
    {
        static ObjectToUrlParameters objectToUrlParameters;
        static string url;
        static StaticObjectWithName parameter;

        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
            parameter = new StaticObjectWithName() {Name="test"};
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(parameter);
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test");       
    }

    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersWithTwoParametersUsingStaticObject
    {
        static ObjectToUrlParameters objectToUrlParameters;
        static string url;
        static StaticObjectWithNameAndId parameter;
        
        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
            parameter = new StaticObjectWithNameAndId() { Name = "test", Id=1 };
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(parameter);
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test&Id=1");
    }

    public class StaticObjectWithName
    {
        public string Name { get; set; }
    }

    public class StaticObjectWithNameAndId
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
