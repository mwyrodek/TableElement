using System;
using System.Runtime.Serialization;

namespace TableElement.Exception
{
    [Serializable]
    public class HeaderNotFoundException : System.Exception
    {
        public HeaderNotFoundException()
        {
        }

        public HeaderNotFoundException(string message) : base(message)
        {
        }

        public HeaderNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected HeaderNotFoundException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}