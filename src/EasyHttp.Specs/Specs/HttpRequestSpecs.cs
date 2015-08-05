#region License
// Distributed under the BSD License
// =================================
// 
// Copyright (c) 2010, Hadi Hariri
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of Hadi Hariri nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// =============================================================
// 
// 
// Parts of this Software use JsonFX Serialization Library which is distributed under the MIT License:
// 
// Distributed under the terms of an MIT-style license:
// 
// The MIT License
// 
// Copyright (c) 2006-2009 Stephen M. McKamey
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

using System;
using System.Net;
using EasyHttp.Http;
using EasyHttp.Specs.Helpers;
using Machine.Specifications;
using Result = EasyHttp.Specs.Helpers.ResultResponse;

namespace EasyHttp.Specs.Specs
{
    [Subject("HttpClient")]
    public class WhenMakingAnyTypeOfRequestToInvalidHost
    {
        static HttpClient httpClient;
        static Exception exception;

        Establish context = () =>
        {
            httpClient = new HttpClient();
        };

        Because of = () =>
        {
            exception = Catch.Exception( () => httpClient.Get("http://somethinginvalid") );
        };

        It shouldThrowWebException  = () => exception.ShouldBeOfType<WebException>();      
    }

    [Subject("HttpClient")]
    public class WhenMakingADeleteRequestWithAValidUri 
    {
        // TODO: Implement me
        static HttpClient httpClient;
        static dynamic response;
        static string rev;
        static Guid guid;
    }
    [Subject("HttpClient")]
    public class WhenMakingAGetRequestWithValidUri
    {

        static HttpClient httpClient;
        static HttpResponse httpResponse;

        Establish context = () =>
        {
            httpClient = new HttpClient();
        };

        Because of = () =>
        {
            httpResponse = httpClient.Get("http://localhost:16000");

        };

        It shouldReturnBodyWithRawtext =
            () => httpResponse.RawText.ShouldNotBeEmpty();    
    }

    [Subject("HttpClient")]
    public class WhenMakingAGetRequestWithValidUriAndAndValidParameters
    {
        static HttpClient httpClient;

        private static dynamic response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
        };

        Because of = () =>
        {
            response = httpClient.Get("http://localhost:16000/hello", new { Name = "true" });
        };

        It shouldReturnDynamicBodyWithJsonObject = () =>
        {
            dynamic body = response.DynamicBody;
            string couchdb = body.Result;
            couchdb.ShouldEqual("Hello, true");
        };
    }

    [Subject("HttpClient")]
    public class WhenMakingAGetRequestWithValidUriAndAndValidParametersUsingSegments
    {
        static HttpClient httpClient;
        static dynamic response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            httpClient.Request.ParametersAsSegments = true;
        };

        Because of = () =>
        {
            response = httpClient.Get("http://localhost:16000/hello", new { Name = "true" });
        };

        It shouldReturnDynamicBodyWithJsonObject = () =>
        {
            dynamic body = response.DynamicBody;
            string couchdb = body.Result;
            couchdb.ShouldEqual("Hello, true");
        };        
    }

    [Subject("HttpClient")]
    public class WhenMakingAGetRequestWithValidUriAndContentTypeSetToApplicationJson
    {

        static HttpClient httpClient;
        static HttpResponse response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
        };

        Because of = () =>
        {
            response = httpClient.Get("http://localhost:16000/hello");
        };

        It shouldReturnDynamicBodyWithJsonObject = () =>
        {
            dynamic body = response.DynamicBody;
            string result = body.Result;
            result.ShouldEqual("Hello, ");
        };

        It shouldReturnStaticBodyWithJsonObject = () =>
        {
            var couchInformation = response.StaticBody<ResultResponse>();
            couchInformation.Result.ShouldEqual("Hello, ");
        };
    }

    [Subject("HttpClient")]
    public class WhenMakingAHeadRequestWithValidUri
    {
        static HttpClient httpClient;
        static HttpResponse response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
        };

        Because of = () =>
        {
            response = httpClient.Head("http://localhost:16000");
        };

        It shouldReturnOkResponse  =
            () => response.StatusDescription.ShouldEqual("OK");      
    }

    [Subject("HttpClient")]
    public class WhenMakingAPostRequestWithValidUriAndValidDataAndContentTypeSetToApplicationJson
    {
        static HttpClient httpClient;
        static dynamic response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
        };

        Because of = () =>
        {
            response = httpClient.Post("http://localhost:16000/hello", new Customer() { Name = "Hadi"}, HttpContentTypes.ApplicationJson);
        };

        It shouldSucceed = () =>
        {
            string id = response.DynamicBody.Result;
            id.ShouldNotBeEmpty();
        };        
    }

    [Subject("HttpClient")]
    public class WhenMakingAPostRequestWithValidUriAndValidDataAndContentTypeSetToApplicationJsonAndParametersAsSegments
    {
        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            httpClient.Request.ParametersAsSegments = true;
        };

        Because of = () =>
        {
            response = httpClient.Post("http://localhost:16000/hello", new Customer() { Name = "Hadi" }, HttpContentTypes.ApplicationJson);
        };

        It shouldSucceed = () =>
        {
            string id = response.DynamicBody.Result;
            id.ShouldNotBeEmpty();
        };

        static HttpClient httpClient;
        static dynamic response;
    }

    [Subject("HttpClient")]
    public class WhenMakingAPutRequestWithValidUriAndValidDataAndContentTypeSetToApplicationJson
    {
        static HttpClient httpClient;
        static dynamic response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
        };

        Because of = () =>
        {
            response = httpClient.Put(string.Format("{0}/{1}", "http://localhost:16000", "hello"),
                          new Customer() { Name = "Put"}, HttpContentTypes.ApplicationJson);
        };

        It shouldSucceed = () =>
        {
            string result = response.DynamicBody.Result;
            result.ShouldNotBeEmpty();
        };
    }

    [Subject("HttpClient")]
    public class WhenMakingRequestsAndPersistingCookies
    {
        static HttpClient httpClient;
        static dynamic response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.PersistCookies = true;
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
        };

        Because of = () =>
        {
            httpClient.Put("http://localhost:16000/cookie", new CookieInfo { Name = "test", Value = "test cookie" }, HttpContentTypes.ApplicationJson);
            response = httpClient.Get("http://localhost:16000/cookie/test");
        };

        It shouldSendReturnedCookies = () =>
        {
            string cookieValue = response.DynamicBody.Value;
            cookieValue.ShouldEqual("test cookie");
        };
    }

    [Subject("HttpClient")]
    public class WhenMakingRequestsAndNotPersistingCookies
    {
        static HttpClient httpClient;
        static dynamic response;

        Establish context = () =>
        {
            httpClient = new HttpClient();
            httpClient.Request.PersistCookies = false;
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
        };

        Because of = () =>
        {
            httpClient.Put("http://localhost:16000/cookie", new CookieInfo { Name = "test", Value = "test cookie" }, HttpContentTypes.ApplicationJson);
            response = httpClient.Get("http://localhost:16000/cookie/test");
        };

        It shouldNotSendReturnedCookies = () =>
        {
            HttpStatusCode statusCode = response.StatusCode;
            statusCode.ShouldEqual(HttpStatusCode.NotFound);
        };      
    }
}