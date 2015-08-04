using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace EasyHttp.Infrastructure
{
    public abstract class ObjectToUrl
    {
        protected abstract string PathStartCharacter
        {
            get;
        }

        protected abstract string PathSeparatorCharacter
        {
            get;
        }

        protected abstract string BuildParam(PropertyValue propertyValue);

        public string ParametersToUrl(object parameters)
        {
            var uri = "";
            var properties = GetProperties(parameters);
            if (parameters != null)
            {
                var paramsList = properties.Select(this.BuildParam).ToList();

                if (paramsList.Count == 0)
                {
                    throw new Exception("The list of parameters is empty.");
                }

                uri = string.Format("{0}{1}", PathStartCharacter, String.Join(PathSeparatorCharacter, paramsList));
            }
            return uri;
        }

        private static IEnumerable<PropertyValue> GetProperties(object parameters)
        {
            if (parameters == null)
            {
                yield break;
            }

            if (parameters is ExpandoObject)
            {
                var dictionary = parameters as IDictionary<string, object>;
                foreach (var property in dictionary)
                {
                    yield return new PropertyValue
                    {
                        Name = property.Key, Value = property.Value.ToString()
                    };
                }
            }
            else
            {
                var properties = TypeDescriptor.GetProperties(parameters);

                foreach (PropertyDescriptor propertyDescriptor in properties)
                {
                    var value = propertyDescriptor.GetValue(parameters);

                    if (value != null)
                    {
                        yield return new PropertyValue
                        {
                            Name = propertyDescriptor.Name, Value = value.ToString()
                        };
                    }
                }
            }
        }

        protected class PropertyValue
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}