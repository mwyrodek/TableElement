using System;

namespace TableElement.Exception
{
    [Serializable()]
    public class HeaderMissMatchException: System.Exception
    {
        public HeaderMissMatchException() { }
        public HeaderMissMatchException(string message) : base(message) { }
        public HeaderMissMatchException(string message, System.Exception inner) : base(message, inner) { }

        protected HeaderMissMatchException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}