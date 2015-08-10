using System;

namespace EasyHttp.Infrastructure
{
    /// <summary>
    /// A class that holds the necessary information and methods to convert an object's properties to URL parameters to be used for the generation of a URL
    /// </summary>
    public class ObjectToUrlParameters : ObjectToUrl
    {
        /// <summary>
        /// The readonly PathStartCharacter property in this class is set to a specific value
        /// </summary>
        protected override string PathStartCharacter
        {
            get
            {
                return "?";
            }
        }

        /// <summary>
        /// The readonly PathSeparatorCharacter property in this class is set to a specific value
        /// </summary>
        protected override string PathSeparatorCharacter
        {
            get
            {
                return "&";
            }
        }

        /// <summary>
        /// The BuildParameters method is overridden to return a specific string to serve as a URL parameter
        /// </summary>
        protected override string ObjectPreparationForParametersConversion(ObjectsWithNameAndValue objectToBeUsed)
        {
            return string.Join("=", objectToBeUsed.Name, System.Web.HttpUtility.UrlEncode(objectToBeUsed.Value));
        }

    }
}