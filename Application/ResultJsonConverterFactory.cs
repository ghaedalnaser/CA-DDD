using Domain.Primitives;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application;

public sealed class ResultJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert == typeof(Result)) return true;
        if (typeToConvert.IsGenericType &&
            typeToConvert.GetGenericTypeDefinition() == typeof(Result<>)) return true;
        return false;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert == typeof(Result))
            return new ResultJsonConverter();

        var valueType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(ResultValueJsonConverter<>).MakeGenericType(valueType);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}

internal sealed class ResultJsonConverter : JsonConverter<Result>
{
    public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var isSuccess = root.GetProperty("IsSuccess").GetBoolean();

        if (isSuccess)
            return Result.Success();

        var code = root.GetProperty("Error").GetProperty("Code").GetString()!;
        var message = root.GetProperty("Error").GetProperty("Message").GetString()!;
        return Result.Failure(new Error(code, message));
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("IsSuccess", value.IsSuccess);
        writer.WriteStartObject("Error");
        writer.WriteString("Code", value.Error.Code);
        writer.WriteString("Message", value.Error.Message);
        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}

internal sealed class ResultValueJsonConverter<T> : JsonConverter<Result<T>>
{
    public override Result<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var isSuccess = root.GetProperty("IsSuccess").GetBoolean();

        if (isSuccess)
        {
            var valueJson = root.GetProperty("Value").GetRawText();
            var value = JsonSerializer.Deserialize<T>(valueJson, options);
            return Result.Success(value!);
        }

        var code = root.GetProperty("Error").GetProperty("Code").GetString()!;
        var message = root.GetProperty("Error").GetProperty("Message").GetString()!;
        return Result.Failure<T>(new Error(code, message));
    }

    public override void Write(Utf8JsonWriter writer, Result<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("IsSuccess", value.IsSuccess);
        writer.WriteStartObject("Error");
        writer.WriteString("Code", value.Error.Code);
        writer.WriteString("Message", value.Error.Message);
        writer.WriteEndObject();
        if (value.IsSuccess)
        {
            writer.WritePropertyName("Value");
            JsonSerializer.Serialize(writer, value.Value, options);
        }
        writer.WriteEndObject();
    }
}
