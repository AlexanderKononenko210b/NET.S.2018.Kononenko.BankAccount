﻿using System;
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
