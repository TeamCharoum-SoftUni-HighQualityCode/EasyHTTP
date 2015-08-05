using System;
using System.Collections.Generic;
using EasyHttp.Codecs;
using EasyHttp.Codecs.JsonFXExtensions;
using EasyHttp.Http;
using JsonFx.Json;
using JsonFx.Json.Resolvers;
using JsonFx.Serialization;
using JsonFx.Serialization.Resolvers;
using JsonFx.Xml.Resolvers;
using Machine.Specifications;

namespace EasyHttp.Specs.BugRepros
{
    [Subject("Custom Decoding")]
    public class WhenDecodingAnObjectWithCustomNamingOfProperty
    {
        static IDecoder decoder;
        static CustomNaming obj;

        static CombinedResolverStrategy CombinedResolverStrategy()
        {
            return new CombinedResolverStrategy(
                new JsonResolverStrategy(),
                new DataContractResolverStrategy(),
                new XmlResolverStrategy(),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.PascalCase),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.CamelCase),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Lowercase, "-"),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Uppercase, "_"));
        }
        Establish context = () =>
        {
            IEnumerable<IDataReader> readers = new List<IDataReader> { new JsonReader(new DataReaderSettings(CombinedResolverStrategy()), HttpContentTypes.ApplicationJson) };            
            decoder = new DefaultDecoder(new RegExBasedDataReaderProvider(readers));
        };

        Because of = () =>
        {
            obj = decoder.DecodeToStatic<CustomNaming>("{\"abc\":\"def\"}", "application/json");
        };

        It shouldDecodeTakingIntoAccountCustomPropertyName = () =>
        {
            obj.PropertyName.ShouldEqual("def");
        };     
    }
    
    [Subject("Custom Encoding")]
    public class WhenEncodingAnObjectWithCustomNamingOfProperty
    {
        static IEncoder encoder;
        static byte[] encoded;

        static CombinedResolverStrategy CombinedResolverStrategy()
        {
            return new CombinedResolverStrategy(
                new JsonResolverStrategy(),
                new DataContractResolverStrategy(),
                new XmlResolverStrategy(),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.PascalCase),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.CamelCase),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Lowercase, "-"),
                new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Uppercase, "_"));
        }

        Establish context = () =>
        {    
            IEnumerable<IDataWriter> writers = new List<IDataWriter> { new JsonWriter(new DataWriterSettings(CombinedResolverStrategy()), HttpContentTypes.ApplicationJson) };
            encoder = new DefaultEncoder(new RegExBasedDataWriterProvider(writers));
        };

        Because of = () =>
        {
            var customObject = new CustomNamedObject {UpperPropertyName = "someValue"};
            encoded = encoder.Encode(customObject, HttpContentTypes.ApplicationJson);
        };

        It shouldDecodeTakingIntoAccountCustomPropertyName = () =>
        {
            var str = System.Text.Encoding.UTF8.GetString(encoded);
            str.ShouldContain("upperPropertyName");
        };     
    }

    public class CustomNamedObject
    {
        [JsonName("upperPropertyName")]
        public string UpperPropertyName { get; set; }
    }


    public class CustomNaming
    {
        [JsonName("abc")]
        public string PropertyName { get; set; }
    }
 
}