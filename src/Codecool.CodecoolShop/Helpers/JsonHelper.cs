using Newtonsoft.Json;
using System.IO;

namespace Codecool.CodecoolShop.Helpers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader stringReader = new StringReader(json);
            JsonTextReader jsonTextReader = new JsonTextReader(stringReader);

            return serializer.Deserialize<T>(jsonTextReader);
        }
    }
}
