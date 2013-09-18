using SchemeCore.helper;
using SchemeCore.objects;
using System;
using System.Collections.Generic;

using System.Text;


namespace SchemeCore.builtin
{
    class SchemeBuiltinCdr :SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != 1 )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Scheme cdr expects exactly on argument of type scheme cons. You have provided: {0}", currentAST.children.Count ) );
            }

            if( ! ( currentAST.children[0].currentObject.GetType() ==  typeof(SchemeList) ) )
            { 
              throw new SchemeInvalidArgumentException( String.Format("Scheme cdr epects an arguement of type scheme cons. Your argument was: {0} of type{1}" , currentAST.children[0].ToString(), currentAST.children[0].GetType().ToString()));
            }


            var cons = (SchemeList) currentAST.children[0].currentObject;

            return cons.cdr;
        }

        public override string ToString()
        {
            return "SchemeBuiltin Cdr"; 
        }
    }
}
