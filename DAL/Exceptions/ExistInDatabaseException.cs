using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    [Serializable]
    public class ExistInDatabaseException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ExistInDatabaseException()
        {
        }

        public ExistInDatabaseException(string message) : base(message)
        {
        }

        public ExistInDatabaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExistInDatabaseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
