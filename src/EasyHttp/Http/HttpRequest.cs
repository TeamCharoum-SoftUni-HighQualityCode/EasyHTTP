#region License

// Distributed under the BSD License
//   
// YouTrackSharp Copyright (c) 2010-2012, Hadi Hariri and Contributors
// All rights reserved.
//   
//  Redistribution and use in source and binary forms, with or without
//  modification, are permitted provided that the following conditions are met:
//      * Redistributions of source code must retain the above copyright
//         notice, this list of conditions and the following disclaimer.
//      * Redistributions in binary form must reproduce the above copyright
//         notice, this list of conditions and the following disclaimer in the
//         documentation and/or other materials provided with the distribution.
//      * Neither the name of Hadi Hariri nor the
//         names of its contributors may be used to endorse or promote products
//         derived from this software without specific prior written permission.
//   
//   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
//   TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
//   PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL 
//   <COPYRIGHTHOLDER> BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
//   SPECIAL,EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
//   LIMITED  TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
//   DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND  ON ANY
//   THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//   (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
//   THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//   

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EasyHttp.Codecs;
using EasyHttp.Infrastructure;

namespace EasyHttp.Http
{
    // TODO: This class needs cleaning up and abstracting the encoder one more level
    public class HttpRequest
    {
        private readonly IEncoder encoder;
        private HttpRequestCachePolicy cachePolicy;
        private HttpWebRequest httpWebRequest;
        private CookieContainer cookieContainer;

        public HttpRequest(IEncoder encoder)
        {
            this.RawHeaders = new Dictionary<string, object>();
            this.ClientCertificates = new X509CertificateCollection();
            this.UserAgent = String.Format("EasyHttp HttpClient v{0}",
                                      Assembly.GetAssembly(typeof(HttpClient)).GetName().Version);

            this.Accept = String.Join(";", HttpContentTypes.TextHtml, HttpContentTypes.ApplicationXml,
                                 HttpContentTypes.ApplicationJson);

            this.encoder = encoder;
            this.Timeout = 100000; //http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.timeout.aspx
            this.AllowAutoRedirect = true;
        }

        public string Accept { get; set; }
        public string AcceptCharSet { get; set; }
        public string AcceptEncoding { get; set; }
        public string AcceptLanguage { get; set; }
        public bool KeepAlive { get; set; }
        public X509CertificateCollection ClientCertificates { get; set; }
        public string ContentLength { get; private set; }
        public string ContentType { get; set; }
        public string ContentEncoding { get; set; }
        public CookieCollection Cookies { get; set; }
        public DateTime Date { get; set; }
        public bool Expect { get; set; }
        public string From { get; set; }
        public string Host { get; set; }
        public string IfMatch { get; set; }
        public DateTime IfModifiedSince { get; set; }
        public string IfRange { get; set; }
        public int MaxForwards { get; set; }
        public string Referer { get; set; }
        public int Range { get; set; }
        public string UserAgent { get; set; }
        public IDictionary<string, object> RawHeaders { get; private set; }
        public HttpMethodType Method { get; set; }
        public object Data { get; set; }
        public string Uri { get; set; }
        public string PutFilename { get; set; }
        public IDictionary<string, object> MultiPartFormData { get; set; }
        public IList<FileData> MultiPartFileData { get; set; }
        public int Timeout { get; set; }
        public bool ParametersAsSegments { get; set; }
        public bool ForceBasicAuthentication { get; set; }
        public bool PersistCookies { get; set; }
        public bool AllowAutoRedirect { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public void SetBasicAuthentication(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public void AddExtraHeader(string header, object value)
        {
            if (value != null && !this.RawHeaders.ContainsKey(header))
            {
                this.RawHeaders.Add(header, value);
            }
        }

        public HttpWebRequest PrepareRequest()
        {
            this.httpWebRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
            this.httpWebRequest.AllowAutoRedirect = this.AllowAutoRedirect;
            this.SetupHeader();
            this.SetupBody();

            return this.httpWebRequest;
        }

        public void SetCacheControlToNoCache()
        {
            this.cachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
        }

        public void SetCacheControlWithMaxAge(TimeSpan maxAge)
        {
            this.cachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, maxAge);
        }

        public void SetCacheControlWithMaxAgeAndMaxStale(TimeSpan maxAge, TimeSpan maxStale)
        {
            this.cachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAgeAndMaxStale, maxAge, maxStale);
        }

        public void SetCacheControlWithMaxAgeAndMinFresh(TimeSpan maxAge, TimeSpan minFresh)
        {
            this.cachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAgeAndMinFresh, maxAge, minFresh);
        }

        private void SetupHeader()
        {
            if (!this.PersistCookies || this.cookieContainer == null)
            {
                this.cookieContainer = new CookieContainer();
            }

            this.httpWebRequest.CookieContainer = this.cookieContainer;
            this.httpWebRequest.ContentType = this.ContentType;
            this.httpWebRequest.Accept = this.Accept;
            this.httpWebRequest.Method = this.Method.ToString();
            this.httpWebRequest.UserAgent = this.UserAgent;
            this.httpWebRequest.Referer = this.Referer;
            this.httpWebRequest.CachePolicy = this.cachePolicy;
            this.httpWebRequest.KeepAlive = this.KeepAlive;
            this.httpWebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

            ServicePointManager.Expect100Continue = this.Expect;
            ServicePointManager.ServerCertificateValidationCallback = this.AcceptAllCertifications;

            if (this.Timeout > 0)
            {
                this.httpWebRequest.Timeout = this.Timeout;
            }

            if (this.Cookies != null)
            {
                this.httpWebRequest.CookieContainer.Add(this.Cookies);
            }

            if (this.IfModifiedSince != DateTime.MinValue)
            {
                this.httpWebRequest.IfModifiedSince = this.IfModifiedSince;
            }

            if (this.Date != DateTime.MinValue)
            {
                this.httpWebRequest.Date = this.Date;
            }

            if (!String.IsNullOrEmpty(this.Host))
            {
                this.httpWebRequest.Host = this.Host;
            }

            if (this.MaxForwards != 0)
            {
                this.httpWebRequest.MaximumAutomaticRedirections = this.MaxForwards;
            }

            if (this.Range != 0)
            {
                this.httpWebRequest.AddRange(this.Range);
            }

            this.SetupAuthentication();
            this.AddExtraHeader("From", this.From);
            this.AddExtraHeader("Accept-CharSet", this.AcceptCharSet);
            this.AddExtraHeader("Accept-Encoding", this.AcceptEncoding);
            this.AddExtraHeader("Accept-Language", this.AcceptLanguage);
            this.AddExtraHeader("If-Match", this.IfMatch);
            this.AddExtraHeader("Content-Encoding", this.ContentEncoding);

            foreach (var header in this.RawHeaders)
            {
                this.httpWebRequest.Headers.Add(String.Format("{0}: {1}", header.Key, header.Value));
            }

        }
        //TODO или да се маха или да се вкара някаква логика
        private bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            return true;
        }

        private void SetupClientCertificates()
        {
            if (this.ClientCertificates == null || this.ClientCertificates.Count == 0)
                return;

            this.httpWebRequest.ClientCertificates.AddRange(this.ClientCertificates);
        }

        private void SetupAuthentication()
        {
            this.SetupClientCertificates();

            if (this.ForceBasicAuthentication)
            {
                string authInfo = this.Username + ":" + this.Password;
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                this.httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            }
            else
            {
                var networkCredential = new NetworkCredential(this.Username, this.Password);
                this.httpWebRequest.Credentials = networkCredential;
            }
        }

        private void SetupBody()
        {
            if (this.Data != null)
            {
                this.SetupData();

                return;
            }

            if (!String.IsNullOrEmpty(this.PutFilename))
            {
                this.SetupPutFilename();
                return;
            }

            if (this.MultiPartFormData != null || this.MultiPartFileData != null)
            {
                this.SetupMultiPartBody();
            }
        }

        private void SetupData()
        {
            var bytes = this.encoder.Encode(this.Data, this.ContentType);

            if (bytes.Length > 0)
            {
                this.httpWebRequest.ContentLength = bytes.Length;
            }

            var requestStream = this.httpWebRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
        }

        private void SetupPutFilename()
        {
            using (var fileStream = new FileStream(this.PutFilename, FileMode.Open))
            {
                this.httpWebRequest.ContentLength = fileStream.Length;

                var requestStream = this.httpWebRequest.GetRequestStream();

                var buffer = new byte[81982];

                int bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                while (bytesRead > 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                }

                requestStream.Close();
            }
        }

        private void SetupMultiPartBody()
        {
            var multiPartStreamer = new MultiPartStreamer(this.MultiPartFormData, this.MultiPartFileData);

            this.httpWebRequest.ContentType = multiPartStreamer.GetContentType();
            var contentLength = multiPartStreamer.GetContentLength();

            if (contentLength > 0)
            {
                this.httpWebRequest.ContentLength = contentLength;
            }

            multiPartStreamer.StreamMultiPart(this.httpWebRequest.GetRequestStream());
        }


        //public HttpWebRequest PrepareRequest()
        //{
        //    this.httpWebRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
        //    this.httpWebRequest.AllowAutoRedirect = this.AllowAutoRedirect;
        //    this.SetupHeader();

        //    this.SetupBody();

        //    return this.httpWebRequest;
        //}

        //void SetupClientCertificates()
        //{
        //    if (this.ClientCertificates == null || this.ClientCertificates.Count == 0)
        //        return;

        //    this.httpWebRequest.ClientCertificates.AddRange(this.ClientCertificates);
        //}

        //void SetupAuthentication()
        //{
        //    this.SetupClientCertificates();

        //    if (this.ForceBasicAuthentication)
        //    {
        //        string authInfo = this.Username + ":" + this.Password;
        //        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        //        this.httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
        //    }
        //    else
        //    {
        //        var networkCredential = new NetworkCredential(this.Username, this.Password);
        //        this.httpWebRequest.Credentials = networkCredential;
        //    }
        //}

        //public void SetCacheControlToNoCache()
        //{
        //    this.cachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
        //}

        //public void SetCacheControlWithMaxAge(TimeSpan maxAge)
        //{
        //    this.cachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAge, maxAge);
        //}

        //public void SetCacheControlWithMaxAgeAndMaxStale(TimeSpan maxAge, TimeSpan maxStale)
        //{
        //    this.cachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAgeAndMaxStale, maxAge, maxStale);
        //}

        //public void SetCacheControlWithMaxAgeAndMinFresh(TimeSpan maxAge, TimeSpan minFresh)
        //{
        //    this.cachePolicy = new HttpRequestCachePolicy(HttpCacheAgeControl.MaxAgeAndMinFresh, maxAge, minFresh);
        //}
    }
}