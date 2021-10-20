using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class InvalidImageSizeException : ValidationException
    {
        public InvalidImageSizeException() : base()
        { }

        public InvalidImageSizeException(string message) : base(message)
        { }

        public InvalidImageSizeException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
