﻿namespace EasyHttp.Specs.Helpers
{
    using System;

    public class HelloResponse
    {
        private string result;

        public string Result
        {
            get
            {
                return this.result;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name can not be null or empty!");
                }
                this.result = value;
            }
        }
    }
}