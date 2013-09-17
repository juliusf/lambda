﻿using SchemeCore.helper;
using SchemeCore.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.builtin
{
    class SchemeBuiltinCar :SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != 1 )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Scheme car expects exactly on argument of type scheme cons. You have provided: {0}", currentAST.children.Count ) );
            }

            if( !( currentAST.children[0].currentObject.GetType() == typeof( SchemeList ) ) )
            {
                throw new SchemeInvalidArgumentException( String.Format( "Scheme car epects an arguement of type scheme cons. Your argument was: {0} of type{1}", currentAST.children[0].ToString(), currentAST.children[0].GetType().ToString() ) );
            }


            var cons = (SchemeList) currentAST.children[0].currentObject;

            return cons.car;
        }

        public override string ToString()
        {
            return "SchemeBuiltin Car";
        }
    }
}
