using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base()
        { }

        public ProductNotFoundException(string message) : base(message)
        { }

        public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
