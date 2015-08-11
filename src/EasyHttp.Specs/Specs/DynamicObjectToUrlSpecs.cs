using System.Dynamic;
using EasyHttp.Http;
using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlParametersWithOneParameterUsingExpandoObject
    {
        private static ObjectToUrlParameters objectToUrlParameters;
        private static dynamic parameters;
        private static string url;

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
        private static ObjectToUrlParameters objectToUrlParameters;
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
        private static ObjectToUrlParameters objectToUrlParameters;
        private static string url;

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
        private static ObjectToUrlParameters objectToUrlParameters;
        private static string url;

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
        private static ObjectToUrlParameters objectToUrlParameters;
        private static string url;

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
        private static ObjectToUrlParameters objectToUrlParameters;
        private static string url;

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
        private static ObjectToUrlParameters objectToUrlParameters;
        private static string url;
        private static StaticObjectWithName parameter;

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
        private static ObjectToUrlParameters objectToUrlParameters;
        private static string url;
        private static StaticObjectWithNameAndId parameter;
        
        Establish context = () =>
        {
            objectToUrlParameters = new ObjectToUrlParameters();
            parameter = new StaticObjectWithNameAndId() { Name = "test", Id=1 };
        };

        Because of = () => url = objectToUrlParameters.ParametersToUrl(parameter);
        It shouldHaveTheCorrectUrlParameters = () => url.ShouldEqual("?Name=test&Id=1");
    }
}
