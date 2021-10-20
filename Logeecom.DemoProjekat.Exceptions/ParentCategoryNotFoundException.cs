using System;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class ParentCategoryNotFoundException : NotFoundException
    {
        public ParentCategoryNotFoundException() : base()
        { }

        public ParentCategoryNotFoundException(string message) : base(message)
        { }

        public ParentCategoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
