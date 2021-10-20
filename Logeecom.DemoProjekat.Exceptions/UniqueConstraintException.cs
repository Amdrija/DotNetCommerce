using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class UniqueConstraintException : ValidationException
    {
        public UniqueConstraintException() : base()
        { }

        public UniqueConstraintException(string message) : base(message)
        { }

        public UniqueConstraintException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
