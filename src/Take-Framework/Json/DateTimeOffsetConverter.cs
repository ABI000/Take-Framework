using System.Text.Json;
using System.Text.Json.Serialization;

namespace TakeFramework.Json
{
    public sealed class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value:yyy-MM-dd HH:mm:ss zzz}");
        }
    }
}
