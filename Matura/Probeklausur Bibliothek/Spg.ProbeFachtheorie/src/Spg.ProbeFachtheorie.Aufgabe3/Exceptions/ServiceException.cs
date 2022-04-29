using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe3.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException()
            : base()
        { }

        public ServiceException(string message)
            : base(message)
        { }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
