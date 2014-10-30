using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerBlocks.Bullets
{
    public class ActionObject
    {
         Guid Uid { get; set; }
            
        int Priority {get; set; } // 1-100
        
        public string TypeName { get; set; }

        public string TypeInfo { get; set; }

        public string ActionInterface { get; set; }

        public string ActionMethod { get; set; }

        public object InnerObject { get; set; }


        public ActionObject()
        {

        }
        public ActionObject(object innerObject)
        {
            InnerObject = innerObject;
        }

    }
}
