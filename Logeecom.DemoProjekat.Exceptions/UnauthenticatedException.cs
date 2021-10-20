using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException() : base()
        { }

        public UnauthenticatedException(string message) : base(message)
        { }

        public UnauthenticatedException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
