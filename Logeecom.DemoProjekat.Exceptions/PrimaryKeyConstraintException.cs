using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class PrimaryKeyConstraintException : ValidationException
    {
        public PrimaryKeyConstraintException() : base()
        { }

        public PrimaryKeyConstraintException(string message) : base(message)
        { }

        public PrimaryKeyConstraintException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
