using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logeecom.DemoProjekat.Exceptions
{
    public class ForeignKeyConstraintException : ValidationException
    {
        public ForeignKeyConstraintException() : base()
        { }

        public ForeignKeyConstraintException(string message) : base(message)
        { }

        public ForeignKeyConstraintException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
