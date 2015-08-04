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
namespace EasyHttp.Http
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    using EasyHttp.Codecs;
    using EasyHttp.Configuration;
    using EasyHttp.Infrastructure;

    public class HttpClient
    {
        private readonly string baseUri;
        private readonly IEncoder encoder;
        private readonly IDecoder decoder;
        private readonly UriComposer uriComposer;

        public HttpClient()
            : this(new DefaultEncoderDecoderConfiguration())
        {
        }

        public HttpClient(string baseUri)
            : this(new DefaultEncoderDecoderConfiguration())
        {
            this.baseUri = baseUri;
        }

        public HttpClient(IEncoderDecoderConfiguration encoderDecoderConfiguration)
        {
            this.encoder = encoderDecoderConfiguration.GetEncoder();
            this.decoder = encoderDecoderConfiguration.GetDecoder();
            this.uriComposer = new UriComposer();

            this.Request = new HttpRequest(this.encoder);
        }

        public HttpResponse Response { get; private set; }

        public HttpRequest Request { get; private set; }

        public bool IsLoggingEnabled { get; set; }

        public bool HasThrowExceptionOnHttpError { get; set; }

        public bool StreamResponse { get; set; }

        public HttpResponse GetAsFile(string uri, string filename)
        {
            this.InitRequest(uri, HttpMethodType.Get, null);
            return this.ProcessRequest(filename);
        }

        public HttpResponse Get(string uri, object query = null)
        {
            this.InitRequest(uri, HttpMethodType.Get, query);
            return this.ProcessRequest();
        }

        public HttpResponse Options(string uri)
        {
            this.InitRequest(uri, HttpMethodType.Options, null);
            return this.ProcessRequest();
        }

        public HttpResponse Post(string uri, object data, string contentType, object query = null)
        {
            this.InitRequest(uri, HttpMethodType.Post, query);
            this.InitData(data, contentType);
            return this.ProcessRequest();
        }

        public HttpResponse Patch(string uri, object data, string contentType, object query = null)
        {
            this.InitRequest(uri, HttpMethodType.Patch, query);
            this.InitData(data, contentType);
            return this.ProcessRequest();
        }

        public HttpResponse Post(string uri, IDictionary<string, object> formData, IList<FileData> files, object query = null)
        {
            this.InitRequest(uri, HttpMethodType.Post, query);
            this.Request.MultiPartFormData = formData;
            this.Request.MultiPartFileData = files;
            this.Request.KeepAlive = true;
            return this.ProcessRequest();
        }

        public HttpResponse Put(string uri, object data, string contentType, object query = null)
        {
            this.InitRequest(uri, HttpMethodType.Put, query);
            this.InitData(data, contentType);
            return this.ProcessRequest();
        }

        public HttpResponse Delete(string uri, object query = null)
        {
            this.InitRequest(uri, HttpMethodType.Delete, query);
            return this.ProcessRequest();
        }

        public HttpResponse Head(string uri, object query = null)
        {
            this.InitRequest(uri, HttpMethodType.Head, query);
            return this.ProcessRequest();
        }

        public HttpResponse PutFile(string uri, string filename, string contentType)
        {
            this.InitRequest(uri, HttpMethodType.Put, null);
            this.Request.ContentType = contentType;
            this.Request.PutFilename = filename;
            this.Request.Expect = true;
            this.Request.KeepAlive = true;
            return this.ProcessRequest();
        }

        public void AddClientCertificates(X509CertificateCollection certificates)
        {
            if (certificates == null || certificates.Count == 0)
            {
                throw new ArgumentNullException("The certifacate's collection is empty!");
            }

            this.Request.ClientCertificates.AddRange(certificates);
        }

        private void InitRequest(string uri, HttpMethodType method, object query)
        {
            this.Request.Uri = this.uriComposer.Compose(this.baseUri, uri, query, this.Request.ParametersAsSegments);
            this.Request.Data = null;
            this.Request.PutFilename = string.Empty;
            this.Request.Expect = false;
            this.Request.KeepAlive = true;
            this.Request.MultiPartFormData = null;
            this.Request.MultiPartFileData = null;
            this.Request.ContentEncoding = null;
            this.Request.Method = method;
        }

        private void InitData(object data, string contentType)
        {
            if (data != null)
            {
                this.Request.ContentType = contentType;
                this.Request.Data = data;
            }
        }

        private HttpResponse ProcessRequest(string filename = "")
        {
            var httpWebRequest = this.Request.PrepareRequest();

            this.Response = new HttpResponse(this.decoder);

            this.Response.GetResponse(httpWebRequest, filename, this.StreamResponse);

            if (this.HasThrowExceptionOnHttpError && this.IsHttpError())
            {
                throw new HttpException(this.Response.StatusCode, this.Response.StatusDescription);
            }

            return this.Response;
        }

        private bool IsHttpError()
        {
            var num = (int)this.Response.StatusCode / 100;

            return num == 4 || num == 5;
        }
    }
}