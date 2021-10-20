using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException() : base()
        { }

        public CategoryNotFoundException(string message) : base(message)
        { }

        public CategoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}