using System;
using System.Collections.Generic;

using System.Text;

using System.Diagnostics;
using SchemeCore.helper;

using SchemeCore.objects;
namespace SchemeCore.builtin
{
    class SchemeBuiltInPlus : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            var list = lookupSymbolsFromEnv( ref currentAST, evaluator.currentEnvironment );
            Debug.Assert(list[0].GetType() == typeof(SchemeBuiltInPlus));
            int sum = 0;
            try
            {
                foreach( SchemeInteger summand in list.GetRange( 1, list.Count - 1 ) ) //all args, but not the current method object
                {
                    sum += summand.value;
                }
            }
            catch( InvalidCastException )
            {
                throw new SchemeInvalidArgumentException("Builtin Function + expects SchemeInteger or SchemeFloat as parameter. Got something else.");
            }
            return new SchemeInteger( sum );
        }
        public override string ToString()
        {
            return "Builtin Function: +";
        }
    }

}
