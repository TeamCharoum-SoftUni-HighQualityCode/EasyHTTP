using EasyHttp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace EasyHttp.Infrastructure
{
    /// <summary>
    /// An abstract class to handle the conversion of objects' properties to URL
    /// </summary>
    public abstract class ObjectToUrl
    {
        protected abstract string PathStartCharacter { get; }
        protected abstract string PathSeparatorCharacter { get; }

        /// <summary>
        /// A method to return a new set of parameters based on provided objects with Name and Value properties to be used in building a URL
        /// </summary>
        protected abstract string ObjectPreparationForParametersConversion(ObjectsWithNameAndValue objectsWithNameAndValue);

        /// <summary>
        /// A method to convert a set of parameters of the class Object to an Uniform Resource Locator (URL)
        /// </summary>
        /// <returns>an url containing the information from the parameters</returns>
        public string ParametersToUrl(object parameters)
        {
            var uri = "";
            var properties = GetProperties(parameters);
            if (parameters != null)
            {
                var parametersList = properties.Select(this.ObjectPreparationForParametersConversion).ToList();

                if (parametersList.Count == 0)
                {
                    throw new NullReferenceException("There are no parameters.");
                }

                uri = string.Format("{0}{1}", this.PathStartCharacter, string.Join(this.PathSeparatorCharacter, parametersList));
            }
            return uri;
        }

        /// <summary>
        /// A method to check the format of parameters and if given in the correct format to prepare them for conversion into an URL.
        /// </summary>
        /// <returns>an enumeration of objects with Name and Value properties to be used in a method converting object information to URL</returns>
        private static IEnumerable<ObjectsWithNameAndValue> GetProperties(object parametersAsExpandoObject)
        {
            if (parametersAsExpandoObject == null)
            {
                yield break;
            }

            if (parametersAsExpandoObject is ExpandoObject)
            {
                var dictionary = parametersAsExpandoObject as IDictionary<string, object>;
                foreach (var property in dictionary)
                {
                    yield return new ObjectsWithNameAndValue
                    {
                        Name = property.Key, Value = property.Value.ToString()
                    };
                }
            }
            else
            {
                var properties = TypeDescriptor.GetProperties(parametersAsExpandoObject);

                foreach (PropertyDescriptor propertyDescriptor in properties)
                {
                    var value = propertyDescriptor.GetValue(parametersAsExpandoObject);

                    if (value != null)
                    {
                        yield return new ObjectsWithNameAndValue
                        {
                            Name = propertyDescriptor.Name, Value = value.ToString()
                        };
                    }
                }
            }
        }

        /// <summary>
        /// A class that facilitates the assignment of name and value of objects.
        /// </summary>
        protected class ObjectsWithNameAndValue
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
                        throw new ArgumentNullException(ReflectionUtility.PropertyName<ObjectsWithNameAndValue>(x => x.Name));
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
                        throw new ArgumentNullException(ReflectionUtility.PropertyName<ObjectsWithNameAndValue>(x => x.Value));
                    }

                    this.value = value;
                }
            }
        }
    }
}