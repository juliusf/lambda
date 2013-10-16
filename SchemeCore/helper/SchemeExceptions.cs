using System;
using System.Collections.Generic;

using System.Text;


namespace SchemeCore.helper
{
    public class SchemeException : System.Exception
    {
        public int offset { get; set; }
        public int length { get; set; }
        public string fileName { get; set; }

        public SchemeException()
        { 
        }

        public SchemeException(string message)
            : base(message)
        { 
        }

        public SchemeException(string message, string file, int lnNr, int colNr)
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
    public class SchemeUndefinedSymbolException :SchemeException
    {
        
        public SchemeUndefinedSymbolException()
        {
            
        }

        public SchemeUndefinedSymbolException( string message )
            : base( message )
        {
        }

        public SchemeUndefinedSymbolException(string message, string file, int length, int offset)
            : base(message)
        {
            this.fileName = file;
            this.offset = offset;
            this.length = length;
        }
    }
    public class SchemeNoSuchMethodException :SchemeException
    {
        public SchemeNoSuchMethodException()
        {
        }

        public SchemeNoSuchMethodException( string message )
            : base( message )
        {
        }
    }

    public class SchemeWrongNumberOfArguments :SchemeException
    {
        public SchemeWrongNumberOfArguments()
        {
        }

        public SchemeWrongNumberOfArguments( string message )
            : base( message )
        {
        }
    }
}
