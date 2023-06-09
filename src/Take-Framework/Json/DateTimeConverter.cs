﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace TakeFramework.Json
{
    public sealed class DateTimeConverter : JsonConverter<DateTime>
    {

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{DateTime.SpecifyKind(value, DateTimeKind.Utc):yyy-MM-dd HH:mm:ss}");
        }
    }
}
