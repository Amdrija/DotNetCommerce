using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class InvalidImageTypeException : ValidationException
    {
        public InvalidImageTypeException() : base()
        { }

        public InvalidImageTypeException(string message) : base(message)
        { }

        public InvalidImageTypeException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
