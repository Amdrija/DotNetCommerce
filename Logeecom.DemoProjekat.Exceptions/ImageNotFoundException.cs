using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class ImageNotFoundException : NotFoundException
    {
        public ImageNotFoundException() : base()
        { }

        public ImageNotFoundException(string message) : base(message)
        { }

        public ImageNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
