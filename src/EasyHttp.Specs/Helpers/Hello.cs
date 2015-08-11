namespace EasyHttp.Specs.Helpers
{
    using System;

    public class Hello
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