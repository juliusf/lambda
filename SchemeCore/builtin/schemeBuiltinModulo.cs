using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SchemeCore.helper;

using SchemeCore.objects;
namespace SchemeCore.builtin
{
    class ScehemBuiltinModulo :SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            var list = lookupSymbolsFromEnv( ref currentAST, evaluator.currentEnvironment );
            Debug.Assert( list[0].GetType() == typeof( ScehemBuiltinModulo ) );

            if( list.Count != 3 )
            { 
            throw new SchemeWrongNumberOfArguments(String.Format("Invalid number of Arguments specified! Builtinfunction modulo expects exactly two parametrs. you specified: {0}", list.Count -1));
            }
            int mod = 0;
            try
            {
              mod = ((SchemeInteger) list[1]).value % ((SchemeInteger) list[2]).value;
            }
            catch( InvalidCastException )
            {
                throw new SchemeInvalidArgumentException( "Builtin Function + expects SchemeInteger or SchemeFloat as parameter. Got something else." );
            }
            return new SchemeInteger( mod );
        }
        public override string ToString()
        {
            return "Builtin Function: +";
        }
    }

}
