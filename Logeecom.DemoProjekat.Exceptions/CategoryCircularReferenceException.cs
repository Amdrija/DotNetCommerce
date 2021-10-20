using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class CategoryCircularReferenceException : ValidationException
    {
        public CategoryCircularReferenceException() : base()
        { }

        public CategoryCircularReferenceException(string message) : base(message)
        { }

        public CategoryCircularReferenceException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
