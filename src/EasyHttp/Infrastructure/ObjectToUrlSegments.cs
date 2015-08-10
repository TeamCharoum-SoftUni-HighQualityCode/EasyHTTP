namespace EasyHttp.Infrastructure
{
    /// <summary>
    /// A class that holds the necessary information and methods to convert an object's properties to URL segments to be later on put together in an URL
    /// </summary>
    public class ObjectToUrlSegments : ObjectToUrl
    {
        /// <summary>
        /// The readonly PathStartCharacter property in this class is set to a specific value
        /// </summary>
        protected override string PathStartCharacter
        {
            get
            {
                return "/";
            }
        }

        /// <summary>
        /// The readonly PathSeparatorCharacter property in this class is set to a specific value
        /// </summary>
        protected override string PathSeparatorCharacter
        {
            get
            {
                return "/";
            }
        }

        /// <summary>
        /// The BuildParameters method is overridden to return a specific string to serve as a URL segment
        /// </summary>
        protected override string ObjectPreparationForParametersConversion(ObjectsWithNameAndValue objectToBeUsed)
        {
            return System.Web.HttpUtility.UrlEncode(objectToBeUsed.Value);
        }
    }
}