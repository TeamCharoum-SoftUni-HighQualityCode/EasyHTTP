namespace EasyHttp.Specs.Helpers
{
    using System;


    /// <summary>
    /// A class that holds the information about cookie 
    /// name and value
    /// </summary>
    public class CookieInfo
    {
        private string name;
        private string value;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(this.name, "Name can not be null or empty!");
                }
                this.name = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(this.value, "Name can not be null or empty!");
                }
                this.value = value;
            }
        }
    }
}