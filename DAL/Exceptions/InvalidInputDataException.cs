using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    [Serializable]
    public class InvalidInputDataException : Exception
    {
        public InvalidInputDataException()
        {
        }

        public InvalidInputDataException(string message) : base(message)
        {
        }

        public InvalidInputDataException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidInputDataException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
