using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarDealershipAssistant.API.DTOs
{
    public class SearchCarsRequest
    {
        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("maxPrice")]
        [JsonConverter(typeof(NullableDecimalStringConverter))]
        public decimal? MaxPrice { get; set; }

        [JsonPropertyName("minYear")]
        [JsonConverter(typeof(NullableIntStringConverter))]
        public int? MinYear { get; set; }

        [JsonPropertyName("fuelType")]
        public string? FuelType { get; set; }

        [JsonPropertyName("transmission")]
        public string? Transmission { get; set; }
    }

    internal sealed class NullableDecimalStringConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => null,
                JsonTokenType.Number when reader.TryGetDecimal(out var value) => value,
                JsonTokenType.String when string.IsNullOrWhiteSpace(reader.GetString()) => null,
                JsonTokenType.String when decimal.TryParse(
                    reader.GetString(),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var parsedValue) => parsedValue,
                _ => throw new JsonException("Unable to parse maxPrice.")
            };
        }

        public override void Write(
            Utf8JsonWriter writer,
            decimal? value,
            JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
                return;
            }

            writer.WriteNullValue();
        }
    }

    internal sealed class NullableIntStringConverter : JsonConverter<int?>
    {
        public override int? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => null,
                JsonTokenType.Number when reader.TryGetInt32(out var value) => value,
                JsonTokenType.String when string.IsNullOrWhiteSpace(reader.GetString()) => null,
                JsonTokenType.String when int.TryParse(
                    reader.GetString(),
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out var parsedValue) => parsedValue,
                _ => throw new JsonException("Unable to parse minYear.")
            };
        }

        public override void Write(
            Utf8JsonWriter writer,
            int? value,
            JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
                return;
            }

            writer.WriteNullValue();
        }
    }
}