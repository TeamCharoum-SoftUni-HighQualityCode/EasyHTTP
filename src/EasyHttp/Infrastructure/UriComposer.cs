namespace EasyHttp.Infrastructure
{
    using System;

    public class UriComposer
    {
        private readonly ObjectToUrlParameters objectToUrlParameters;
        private readonly ObjectToUrlSegments objectToUrlSegments;

        public UriComposer()
        {
            this.objectToUrlParameters = new ObjectToUrlParameters();
            this.objectToUrlSegments = new ObjectToUrlSegments();
        }

        public string Compose(string baseuri, string uri, object query, bool parametersAreSegments)
        {
            var returnUri = uri;
            if (!string.IsNullOrEmpty(baseuri))
            {
                returnUri = baseuri.EndsWith("/") ? baseuri : string.Concat(baseuri,"/");
                returnUri += uri.StartsWith("/", StringComparison.InvariantCulture) ? uri.Substring(1) : uri;
            }

            if (parametersAreSegments)
            {
                returnUri = query != null ? string.Concat(returnUri, this.objectToUrlSegments.ParametersToUrl(query)) : returnUri;
            }
            else
            {
                returnUri = query != null ? string.Concat(returnUri, this.objectToUrlParameters.ParametersToUrl(query)) : returnUri;
            }

            return returnUri;
        }
    }
}