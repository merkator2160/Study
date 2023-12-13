using Newtonsoft.Json;
using System.Globalization;

namespace Common.Contracts.Attributes
{
    public class FromDateFormatConverter : JsonConverter
    {
        private readonly String _dateFormat;


        public FromDateFormatConverter(String format)
        {
            _dateFormat = format;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public static DateTime Default => DateTime.UtcNow.AddYears(-1).Date;


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public override Boolean CanConvert(Type type)
        {
            return type == typeof(String);
        }
        public override Object ReadJson(JsonReader reader, Type type, Object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return Default;

            var value = reader.Value.ToString();

            return DateTime.ParseExact(value, _dateFormat, null, DateTimeStyles.AssumeUniversal);
        }
        public override void WriteJson(JsonWriter writer, Object untypedValue, JsonSerializer serializer)
        {
            var date = (DateTime)untypedValue;
            var dateStr = date.ToString("dd.MM.yyyy");

            writer.WriteValue(dateStr);
        }
    }
}