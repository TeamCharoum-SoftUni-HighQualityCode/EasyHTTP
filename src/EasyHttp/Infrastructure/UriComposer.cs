using System;

namespace EasyHttp.Infrastructure
{
    public class UriComposer
    {
        readonly ObjectToUrlParameters objectToUrlParameters;
        private readonly ObjectToUrlSegments objectToUrlSegments;

        public UriComposer()
        {
            this.objectToUrlParameters = new ObjectToUrlParameters();
            this.objectToUrlSegments = new ObjectToUrlSegments();
        }

        public string Compose(string baseuri, string uri, object query, bool parametersAsSegments)
        {
            var returnUri = uri;
            if(!String.IsNullOrEmpty(baseuri))
            {
                returnUri = baseuri.EndsWith("/") ? baseuri : String.Concat(baseuri,"/");
                returnUri += uri.StartsWith("/", StringComparison.InvariantCulture) ? uri.Substring(1) : uri;
            }
            if (parametersAsSegments)
            {
                returnUri = query != null ? String.Concat(returnUri, this.objectToUrlSegments.ParametersToUrl(query)) : returnUri;
            }
            else
            {
                returnUri = query != null ? String.Concat(returnUri, this.objectToUrlParameters.ParametersToUrl(query)) : returnUri;
            }
            return returnUri;
        }
    }
}