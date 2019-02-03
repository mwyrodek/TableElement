using System;

namespace TableElement.Exception
{
    [Serializable()]
    public class HeaderMissMatch: System.Exception
    {
        public HeaderMissMatch() : base() { }
        public HeaderMissMatch(string message) : base(message) { }
        public HeaderMissMatch(string message, System.Exception inner) : base(message, inner) { }

        protected HeaderMissMatch(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}