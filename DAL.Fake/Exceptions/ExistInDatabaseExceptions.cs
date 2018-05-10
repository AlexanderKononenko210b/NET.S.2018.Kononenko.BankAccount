using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Fake.Exceptions
{
    /// <summary>
    /// Exceptions if model already exist in repository
    /// </summary>
    [Serializable]
    public class ExistInDataBaseException : Exception
    {

        public ExistInDataBaseException()
        {
        }

        public ExistInDataBaseException(string message) : base(message)
        {
        }

        public ExistInDataBaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExistInDataBaseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
