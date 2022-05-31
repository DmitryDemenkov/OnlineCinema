using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException() : base() { }
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception inner) : base(message, inner) { }

        public RepositoryException(int code, string message) : base(message)
        {
            ErrorCode = code;
        }

        protected RepositoryException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public int ErrorCode { get; private set; }
    }
}
