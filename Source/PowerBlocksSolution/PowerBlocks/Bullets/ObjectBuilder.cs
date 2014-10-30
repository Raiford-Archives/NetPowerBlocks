using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerBlocks.Bullets
{
    public class ObjectBuilder<T>
    {
        Guid Uid { get; set; }

        int Priority { get; set; } // 1-100

        public string TypeName { get; set; }

        public string TypeInfo { get; set; }

        public string ActionInterface { get; set; }

        public string ActionMethod { get; set; }

        public string ObjectData { get; set; }

        public T ObjectInstance { get; set; }

        public ObjectBuilder(T obj)
        {


        }

    }
}
