using System;

namespace TableElement.Exception
{
    [Serializable()]
    public class TableNotFoundException : System.Exception
    {
        public TableNotFoundException()
        { }
        public TableNotFoundException(string message) : base(message) { }
        public TableNotFoundException(string message, System.Exception inner) : base(message, inner) { }

        protected TableNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}