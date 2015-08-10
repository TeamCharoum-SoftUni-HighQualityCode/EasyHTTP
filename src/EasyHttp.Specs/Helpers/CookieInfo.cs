namespace EasyHttp.Specs.Helpers
{
    using System;

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
                    throw new ArgumentNullException("Name can not be null or empty!");
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
                    throw new ArgumentNullException("Name can not be null or empty!");
                }
                this.value = value;
            }
        }
    }
}