using Newtonsoft.Json;

namespace BTCWebWallet.Helpers;

public class BoolObjectConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
       if (reader.TokenType == JsonToken.Boolean)
       {
           if ((bool)reader.Value == false)
               return null;
       }

       return serializer.Deserialize(reader, objectType);
    }

    public override bool CanConvert(Type objectType)
    {
        return false;
    }
}