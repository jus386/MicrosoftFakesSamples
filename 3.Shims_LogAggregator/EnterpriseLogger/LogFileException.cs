using System;
using System.Runtime.Serialization;

namespace Logger.LogAggregator
{
    public class LogFileException : Exception
    {
        public LogFileException()
        {
        }

        public LogFileException(string message) : base(message)
        {
        }

        public LogFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LogFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
