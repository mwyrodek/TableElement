using System;
using System.Runtime.Serialization;

namespace TableElement.Exception
{
    [Serializable]
    public class TableNotFoundException : System.Exception
    {
        public TableNotFoundException()
        {
        }

        public TableNotFoundException(string message) : base(message)
        {
        }

        public TableNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected TableNotFoundException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}