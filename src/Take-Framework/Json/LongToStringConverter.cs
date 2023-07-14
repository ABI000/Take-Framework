using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TakeFramework.Json
{
    public class LongToStringConverter : JsonConverter<long>
    {

        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string formatted = reader.GetString()!;

            if (!long.TryParse(formatted, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out long vule))
            {
                throw new JsonException();
            }

            return vule;
        }


        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            string formatted = FormattableString.Invariant($"{value}");
            writer.WriteStringValue(formatted);
        }
    }
}
