using System;
using System.Runtime.Serialization;

namespace TableElement.Exception
{
    [Serializable]
    public class HeaderMissMatchException : System.Exception
    {
        public HeaderMissMatchException()
        {
        }

        public HeaderMissMatchException(string message) : base(message)
        {
        }

        public HeaderMissMatchException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected HeaderMissMatchException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}