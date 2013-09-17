using SchemeCore.objects;
using SchemeCore.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchemeCore.builtin
{
    class SchemeBuiltinCons : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != 2 )
            { 
            throw new SchemeWrongNumberOfArguments(String.Format("Scheme Cons expects exactly 2  arguments. You have given me: {0}", currentAST.children.Count -1)); 
            }
            var objects = lookupSymbolsFromEnv( ref currentAST, evaluator.currentEnvironment );

            return new SchemeList( objects[1], objects[2] );
        }

        public override string ToString()
        {
            return "SchemeBuiltin Cons";
        }
    }
}
