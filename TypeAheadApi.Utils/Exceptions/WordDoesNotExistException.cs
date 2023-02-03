using System;
using System.Runtime.Serialization;

namespace TypeAheadApi.Utils.Exceptions
{
    [Serializable]
    public class WordDoesNotExistException : ApplicationException
    {
        public WordDoesNotExistException(string word) : base(string.Format(Messages.WORD_DOES_NOT_EXIST, word)) { }
        protected WordDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
