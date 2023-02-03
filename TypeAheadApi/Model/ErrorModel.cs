using System;

namespace TypeAheadApi.Model
{
    [Serializable]
    public class ErrorModel
    {
        public string Message { get; set; }

        public ErrorModel(string message)
        {
            Message = message;
        }

        public ErrorModel(Exception exception)
        {
            Message = exception.Message;
        }
    }
}