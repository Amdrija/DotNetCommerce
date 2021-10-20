using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class ImageAlreadyTakenException : ValidationException
    {
        public ImageAlreadyTakenException() : base()
        { }

        public ImageAlreadyTakenException(string message) : base(message)
        { }

        public ImageAlreadyTakenException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
