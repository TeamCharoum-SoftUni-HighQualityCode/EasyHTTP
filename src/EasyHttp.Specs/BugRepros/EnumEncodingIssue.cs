using System.Collections.Generic;
using EasyHttp.Codecs;
using EasyHttp.Codecs.JsonFXExtensions;
using EasyHttp.Http;
using JsonFx.Json;
using JsonFx.Serialization;
using Machine.Specifications;




namespace EasyHttp.Specs.BugRepros
{
    [Subject("Encoding Enums")]
    public class WhenEncodingAnObjectThatContainsAnEnum
    {
        static HttpClient client;
        static DefaultEncoder encoder;
        static byte[] result;

        Establish context = () =>
        {
            IEnumerable<IDataWriter> writers = new List<IDataWriter> { new JsonWriter(new DataWriterSettings(), "application/.*json") };

            encoder = new DefaultEncoder(new RegExBasedDataWriterProvider(writers));
        };

        Because of = () =>
        {
            var data = Bar.First;
            result = encoder.Encode(data, "application/vnd.fubar+json");
        };

        It shouldEncodeCorrectly = () =>
        {
            result.Length.ShouldBeGreaterThan(0);
        };       
    }  
}
