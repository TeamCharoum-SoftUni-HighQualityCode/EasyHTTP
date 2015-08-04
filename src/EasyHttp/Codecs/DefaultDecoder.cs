namespace EasyHttp.Codecs
{
    using System;
    using JsonFx.Serialization;
    using JsonFx.Serialization.Providers;

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

        private static string NormalizeInputRemovingAmpersands(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(input, "Input can not be null or empty!");
            }

            var parsedText = input.Replace("\"@", "\"");
            return parsedText;
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
    }
}