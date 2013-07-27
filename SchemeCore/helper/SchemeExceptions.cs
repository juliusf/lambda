using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.helper
{
    public class SchemeException : System.Exception
    {
        public SchemeException()
        { 
        }

        public SchemeException(string message)
            : base(message)
        { 
        }
    }

    public class SchemeInvalidArgumentException : SchemeException
    { 
           public SchemeInvalidArgumentException()
        { 
        }

           public SchemeInvalidArgumentException(string message)
            : base(message)
        { 
        }
    }
}
