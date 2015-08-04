using System;
using System.IO;
using JsonFx.Serialization;
using JsonFx.Serialization.Providers;

namespace EasyHttp.Codecs
{
    public class DefaultDecoder: IDecoder
    {
        readonly IDataReaderProvider DataReaderProvider;

        public DefaultDecoder(IDataReaderProvider dataReaderProvider)
        {
            this.DataReaderProvider = dataReaderProvider;
        }

        public T DecodeToStatic<T>(string input, string contentType)
        {

            var parsedText = NormalizeInputRemovingAmpersands(input);

            var deserializer = this.ObtainDeserializer(contentType);

            return deserializer.Read<T>(parsedText);

        }

        public dynamic DecodeToDynamic(string input, string contentType)
        {
            var parsedText = NormalizeInputRemovingAmpersands(input);

            var deserializer = this.ObtainDeserializer(contentType);
       
            return deserializer.Read(parsedText);
        }

        IDataReader ObtainDeserializer(string contentType)
        {
            var deserializer = this.DataReaderProvider.Find(contentType);


            if (deserializer == null)
            {
                throw new SerializationException("The encoding requested does not have a corresponding decoder");
            }
            return deserializer;
        }

		  static string NormalizeInputRemovingAmpersands(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            var parsedText = input.Replace("\"@", "\"");
            return parsedText;
        }
    }
}