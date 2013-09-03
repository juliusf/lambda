using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore.objects;
using SchemeCore.helper;

namespace SchemeCore.builtin
{
    class SchemeBuitInDefine :SchemeBuiltInFunction
    {

        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != 2 )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Wrong number of arguments!'define' expects exactly two arguments. You provided: {0}", currentAST.children.Count ) );  
            }

           
                var param = currentAST.children[1];

                SchemeObject value;
            if( param.currentObject.GetType() == typeof( SchemeSymbol ) )
            {
                 value = lookupSymbolsFromEnv( ref param, evaluator.currentEnvironment )[0];
            }
            else
            {
                 value = param.currentObject;
            }


            evaluator.currentEnvironment.set( (SchemeSymbol) currentAST.children[0].currentObject, (SchemeType) value );

            return SchemeVoid.instance;
        }

        public override string ToString()
        {
            return "builtin syntax: define";
        }
    }
}
