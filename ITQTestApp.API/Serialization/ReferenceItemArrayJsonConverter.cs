using ITQTestApp.API.Contracts.Requests;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ITQTestApp.API.Serialization
{
    public sealed class ReferenceItemArrayJsonConverter : JsonConverter<ReplaceReferenceItemsRequest>
    {
        public override ReplaceReferenceItemsRequest Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Ожидается JSON-массив.");

            var result = new Dictionary<int, string>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException("Ожидается объект вида { \"<code>\": \"<value>\" }.");

                // Читаем объект и требуем ровно одно свойство
                reader.Read();
                if (reader.TokenType == JsonTokenType.EndObject)
                    throw new JsonException("Объект не должен быть пустым. Ожидается одно свойство.");

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("Ожидается имя свойства (код).");

                var codeAsString = reader.GetString();
                if (string.IsNullOrWhiteSpace(codeAsString))
                    throw new JsonException("Код не задан.");

                if (!int.TryParse(codeAsString, out var code))
                    throw new JsonException($"Код '{codeAsString}' должен быть целым числом.");

                // Значение свойства
                reader.Read();
                string? value;

                if (reader.TokenType == JsonTokenType.String)
                {
                    value = reader.GetString();
                }
                else if (reader.TokenType == JsonTokenType.Null)
                {
                    value = null;
                }
                else
                {
                    throw new JsonException("Значение должно быть строкой.");
                }

                // После значения должен быть конец объекта
                reader.Read();
                if (reader.TokenType != JsonTokenType.EndObject)
                    throw new JsonException("Объект должен содержать ровно одно свойство.");

                result.Add(code, value!);
            }

            return new ReplaceReferenceItemsRequest { Items = result};
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReplaceReferenceItemsRequest value,
            JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value.Items)
            {
                writer.WriteStartObject();
                writer.WriteString(item.Key.ToString(), item.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
