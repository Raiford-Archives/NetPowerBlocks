using PowerBlocks.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerBlocks.Bullets
{
    public class ActionObjectSerializer
    {
        
        public static string Serialize(ActionObject obj)
        {
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string json = serializer.Serialize<ActionObject>(obj);
            return json;
        }

        public static ActionObject Deserialize(string json)
        {
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            ActionObject actionObject = serializer.Deserialize<ActionObject>(json);
            return actionObject;

        }

        public static T DeserializeInner<T>(string json) where T:class
        {


            ActionObject actionObject = Deserialize(json);
            object o = actionObject.InnerObject;
            string t = o.GetType().FullName;
            T innerObject = actionObject.InnerObject as T;

            return innerObject;

        }

    }
}
