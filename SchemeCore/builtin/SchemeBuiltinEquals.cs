using SchemeCore.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SchemeCore.helper;

namespace SchemeCore.builtin
{
    class SchemeBuiltinEquals  : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            var list = lookupSymbolsFromEnv( ref currentAST, evaluator.currentEnvironment );
            Debug.Assert( list[0].GetType() == typeof( SchemeBuiltinEquals ) );

            if( list.Count < 3 )
            { 
                throw new SchemeWrongNumberOfArguments(String.Format("Too few arguments! SchemeEquals need at least 2 arguments. You specified: {0}", list.Count -1)); 
            }

            var firstVal = list[1];
            if( firstVal.GetType() != typeof( SchemeInteger ) )
            {
                throw new SchemeInvalidArgumentException( String.Format( "Invalid Argument Exception! SchemeEquals expects numbers as arugments. You provided: {0}", firstVal.GetType().ToString() ) );
            }

            try
            {
                foreach( SchemeInteger value in list.GetRange( 2, list.Count - 2 ) )
                {
                   if (! firstVal.Equals(value) )
                   {
                   return SchemeFalse.instance;
                   }
                }
            }
            catch( InvalidCastException )
            {
                throw new SchemeInvalidArgumentException( "Builtin Function + expects SchemeInteger or SchemeFloat as parameter. Got something else." );
            }

            return SchemeTrue.instance;
        }

        public override string ToString()
        {
            return "SchemeBuilitin equals" ;
        }
    }
}
