using System;

namespace TableElement.Exception
{
    [Serializable()]
    public class HeaderNotFoundException : System.Exception
    {
        public HeaderNotFoundException()
        { }
        public HeaderNotFoundException(string message) : base(message) { }
        public HeaderNotFoundException(string message, System.Exception inner) : base(message, inner) { }

        protected HeaderNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}