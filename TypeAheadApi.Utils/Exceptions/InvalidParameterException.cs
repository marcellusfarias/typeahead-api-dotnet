using System;
using System.Runtime.Serialization;

namespace TypeAheadApi.Utils.Exceptions
{
    [Serializable]
    public class InvalidParameterException : ApplicationException
    {
        public InvalidParameterException() : base(Messages.INVALID_PARAMETERS) { }
        public InvalidParameterException(string message) : base(message) { }
        protected InvalidParameterException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
