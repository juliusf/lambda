﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SchemeCore.objects;
namespace SchemeCore.builtin
{
    class SchemeBuiltInPlus : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, ISchemeEnvironment environment )
        {
            var list = lookupSymbolsFromEnv( ref currentAST, environment );
            Debug.Assert(list[0].GetType() == typeof(SchemeBuiltInPlus));
            int sum = 0;
            foreach( SchemeInteger summand in list.GetRange( 1, list.Count - 1 ) ) //all args, but not the current method object
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
