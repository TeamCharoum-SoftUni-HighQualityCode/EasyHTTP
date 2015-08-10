namespace EasyHttp.Specs.Helpers
{
    using System;

    public class Files
    {
        private string name;

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
    }
}