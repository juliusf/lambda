using SchemeCore.helper;
using SchemeCore.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.builtin
{
    class SchemeBuiltinCdr :SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != 1 )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Scheme car expects exactly on argument of type scheme cons. You have provided: {0}", currentAST.children.Count ) );
            }

            return null;
        }

        public override string ToString()
        {
            return "SchemeBuiltin Cdr"; 
        }
    }
}
