using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.objects
{
    class SchemeNil : SchemeObject
    {
        private static readonly SchemeNil instance = new SchemeNil();

        private SchemeNil() { }

        public static SchemeNil Instance { get { return instance; } }

        public override string ToString()
            {
 	            return "()";
            }
    }
    
}
