using Newtonsoft.Json;
using NodaTime.Serialization.JsonNet;

namespace Training.Common.AspNetCore.Utils;

public sealed class ExtendedLocalDateConverter : DelegatingConverterBase
{
    public ExtendedLocalDateConverter()
        : base(NodaConverters.LocalDateConverter)
    { }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var valueType = reader.ValueType;
        var value = reader.Value as string;
        if (valueType == typeof(string) && value != default)
        {
            var convertedValue = value.Split("T")[0];
            using var newReader = new JsonTextReader(new StringReader($@"""{convertedValue}"""));
            newReader.Read();
            return base.ReadJson(newReader, objectType, existingValue, serializer);
        }
        else
        {
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
