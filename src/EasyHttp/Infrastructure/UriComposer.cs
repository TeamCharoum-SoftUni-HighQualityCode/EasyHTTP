namespace EasyHttp.Infrastructure
{
    using System;

    /// <summary>
    /// A class to handle the way an URI is put together based on input object properties as parameters
    /// </summary>
    public class UriComposer
    {
        private readonly ObjectToUrlParameters objectToUrlParameters;
        private readonly ObjectToUrlSegments objectToUrlSegments;

        public UriComposer()
        {
            this.objectToUrlParameters = new ObjectToUrlParameters();
            this.objectToUrlSegments = new ObjectToUrlSegments();
        }

        /// <summary>
        /// The main method in this class - it composes an URI based on input data, including an input of object (instead of direct parameters)
        /// </summary>
        /// <param name="baseuri">the beginning of the URI</param>
        /// <param name="uri"></param>
        /// <param name="query">the object whose parameters' information will be added to the uri</param>
        /// <param name="areParametersSegments">a check whether the parameters are already in segment format or not</param>
        /// <returns>the final Uniform Resource Identifier</returns>
        public string Compose(string baseuri, string uri, object query, bool areParametersSegments)
        {
            var returnUri = uri;
            if (!string.IsNullOrEmpty(baseuri))
            {
                returnUri = baseuri.EndsWith("/") ? baseuri : string.Concat(baseuri,"/");
                returnUri += uri.StartsWith("/", StringComparison.InvariantCulture) ? uri.Substring(1) : uri;
            }

            if (areParametersSegments)
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