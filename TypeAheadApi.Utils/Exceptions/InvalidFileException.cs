using System;
using System.Runtime.Serialization;

namespace TypeAheadApi.Utils.Exceptions
{
    [Serializable]
    public class InvalidFileException : ApplicationException
    {
        public InvalidFileException() : base(Messages.INVALID_PARAMETERS) { }
        protected InvalidFileException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
