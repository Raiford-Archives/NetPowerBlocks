using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerBlocks.Serialization
{
    public class JsonObjectSerializer
    {

        public string Serialize<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return json;
        }

        public T Deserialize<T>(string json)
        {
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

    }
}
