using System;
using System.Runtime.Serialization;

namespace HitMeApp.Users.Exceptions
{
    internal class PasswordNotValidException : Exception
    {
        public PasswordNotValidException()
        {
        }

        public PasswordNotValidException(string message) : base(message)
        {
        }

        public PasswordNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
