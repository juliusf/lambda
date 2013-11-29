using SchemeCore.objects;
using System;
using System.Collections.Generic;

using System.Text;

using System.Diagnostics;
using SchemeCore.helper;

namespace SchemeCore.builtin
{
    class SchemeBuiltinGreater  : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            var list = lookupSymbolsFromEnv( ref currentAST, evaluator.currentEnvironment );
            Debug.Assert( list[0].GetType() == typeof( SchemeBuiltinGreater ) );

            if( list.Count < 3 )
            { 
                throw new SchemeWrongNumberOfArguments(String.Format("Too few arguments! SchemeEquals need at least 2 arguments. You specified: {0}", list.Count -1)); 
            }

            SchemeInteger firstVal;
            try
            {
              firstVal = (SchemeInteger )list[1];
            }
            catch( InvalidCastException )
            {
                throw new SchemeInvalidArgumentException( "Builtin Function > expects SchemeInteger or SchemeFloat as parameter. Got something else." );
            }


            try
            {
                foreach( SchemeInteger value in list.GetRange( 2, list.Count - 2 ) )
                {
                   if (! (firstVal.value > value.value) )
                   {
                   return SchemeFalse.instance;
                   }
                   firstVal = value;
                }
            }
            catch( InvalidCastException )
            {
                throw new SchemeInvalidArgumentException( "Builtin Function > expects SchemeInteger or SchemeFloat as parameter. Got something else." );
            }

            return SchemeTrue.instance;
        }

        public override string ToString()
        {
            return "SchemeBuilitin >" ;
        }
    }
}
