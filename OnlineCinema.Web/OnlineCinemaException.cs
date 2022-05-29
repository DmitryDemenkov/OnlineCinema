using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web
{
    [Serializable]
    public class OnlineCinemaException : Exception
    {
        public OnlineCinemaException() : base() { }
        public OnlineCinemaException(string message) : base(message) { }
        public OnlineCinemaException(string message, Exception inner) : base(message, inner) { }

        public OnlineCinemaException(int code, string message) : base(message)
        {
            ErrorCode = code;
        }

        protected OnlineCinemaException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public int ErrorCode { get; set; }
    }
}
