using System;

namespace TableElement.Exception
{
    [Serializable()]
    public class HeaderNotFound : System.Exception
    {
        public HeaderNotFound() : base() { }
        public HeaderNotFound(string message) : base(message) { }
        public HeaderNotFound(string message, System.Exception inner) : base(message, inner) { }

        protected HeaderNotFound(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}