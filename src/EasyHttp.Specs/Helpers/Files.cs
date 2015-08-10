namespace EasyHttp.Specs.Helpers
{
    using System;


    /// <summary>
    /// A class that holds the information about file name
    /// </summary>
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
                    throw new ArgumentNullException(this.name, "Name can not be null or empty!");
                }
                this.name = value;
            }
        }
    }
}