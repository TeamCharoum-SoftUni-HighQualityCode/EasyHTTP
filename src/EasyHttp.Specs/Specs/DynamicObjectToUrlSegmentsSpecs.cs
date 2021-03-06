﻿using System.Dynamic;
using EasyHttp.Http;
using EasyHttp.Infrastructure;
using Machine.Specifications;

namespace EasyHttp.Specs.Specs
{
    [Subject(typeof(HttpClient))]
    public class WhenMakingUrlSegmentsWithOneParameterUsingExpandoObject
    {
        private static ObjectToUrlSegments objectToUrlSegments;
        private static dynamic parameters;
        private static string url;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static dynamic parameters;
        private static string url;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static string url;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static string url;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static string url;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static string url;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static string url;
        private static StaticObjectWithName parameter;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static string url;
        private static StaticObjectWithNameAndId parameter;

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
        private static ObjectToUrlSegments objectToUrlSegments;
        private static string url;
        private static StaticObject parameter;

        Establish context = () =>
        {
            objectToUrlSegments = new ObjectToUrlSegments();
            parameter = new StaticObject() { Name = "test", Id = 1 };
        };
        Because of = () => url = objectToUrlSegments.ParametersToUrl(parameter);
        It shouldHaveTheCorrectUrlSegments = () => url.ShouldEqual("/1/test");     
    }
}
