using System;
using System.Runtime.Serialization;

namespace HitMeApp.Users.Exceptions
{
    internal class EmailNotValidException : Exception
    {
        public EmailNotValidException()
        {
        }

        public EmailNotValidException(string message) : base(message)
        {
        }

        public EmailNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
