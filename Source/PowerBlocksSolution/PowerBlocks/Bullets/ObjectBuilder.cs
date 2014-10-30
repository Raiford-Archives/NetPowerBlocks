using PowerBlocks.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerBlocks.Bullets
{
    public class ObjectBuilder
    {
       
        private object _objectInstance = null;
        private ActionObject _actionObject = new ActionObject();
        private string _json = "";
        
        
        
        //    Guid Uid { get; set; }

    //    int Priority { get; set; } // 1-100

    //    public string TypeName { get; set; }

    //    public string TypeInfo { get; set; }

    //    public string ActionInterface { get; set; }

    //    public string ActionMethod { get; set; }

    //    public string ObjectData { get; set; }

    //    public T ObjectInstance { get; set; }

        public ObjectBuilder(object obj)
        {
            _objectInstance = obj;

        }

        public ObjectBuilder(string json)
        {
            _json = json;
        }

        public void Build()
        {          
            _actionObject.TypeName = _objectInstance.GetType().FullName;
            _actionObject.InnerObject = this._objectInstance;            
        }



          public void AddInnerObject(object obj)
          {
              _actionObject.InnerObject = obj;
          }

        
          public void AddActionObject(ActionObject actionObject)
          {
              this._actionObject = actionObject;

          }

     
            // To Jsonb
            //string json = builder.ToJson();

            //// Convert to various formats
            //builder.ToActionObject();
            //builder.ToInnerObject();
            //builder.ToEncryptedString();
            //builder.ToJson();
            //builder.ToString(); /


        public string ToJson()
        {
            EnsureReady();

            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string json = serializer.Serialize<ActionObject>(_actionObject);
            return json;

        }

        
        /// <summary>
        /// Ensure the Object is in a state to Serialize
        /// </summary>
        private void EnsureReady()
        {
            if (this._actionObject == null) throw new InvalidOperationException();
            if (this._objectInstance == null) throw new InvalidOperationException();

        }

    }
}
