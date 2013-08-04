using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore.objects;
namespace SchemeCore.builtin
{
    class SchemeBuiltInPlus : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( List<SchemeObject> parameters )
        {
            int sum = 0;
            foreach ( SchemeInteger summand in parameters )
            { 
            sum += summand.value;
            }
            return new SchemeInteger( sum );
        }
        public override string ToString()
        {
            return "Builtin Function: +";
        }
    }

}
