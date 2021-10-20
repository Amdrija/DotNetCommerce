using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class SKUExsistsException : ValidationException
    {
        public SKUExsistsException() : base()
        { }

        public SKUExsistsException(string message) : base(message)
        { }

        public SKUExsistsException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
