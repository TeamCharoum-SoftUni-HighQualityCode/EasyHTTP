using System;
using System.IO;
using JsonFx.Serialization;
using JsonFx.Serialization.Providers;

namespace EasyHttp.Codecs
{
    public class DefaultDecoder : IDecoder
    {
        private readonly IDataReaderProvider dataReaderProvider;

        public DefaultDecoder(IDataReaderProvider dataReaderProvider)
        {
            this.dataReaderProvider = dataReaderProvider;
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

        private IDataReader ObtainDeserializer(string contentType)
        {
            var deserializer = this.dataReaderProvider.Find(contentType);


            if (deserializer == null)
            {
                throw new SerializationException("The encoding requested does not have a corresponding decoder");
            }
            return deserializer;
        }

        private static string NormalizeInputRemovingAmpersands(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Input can not be null or empty!");
            }

            var parsedText = input.Replace("\"@", "\"");
            return parsedText;
        }
    }
}