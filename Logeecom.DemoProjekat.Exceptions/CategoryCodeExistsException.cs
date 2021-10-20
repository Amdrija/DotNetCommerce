using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class CategoryCodeExistsException : ValidationException
    {
        public CategoryCodeExistsException() : base()
        { }

        public CategoryCodeExistsException(string message) : base(message)
        { }

        public CategoryCodeExistsException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
