﻿using SchemeCore.objects;
using System;
using System.Collections.Generic;

using System.Text;

using System.Diagnostics;
using SchemeCore.helper;

namespace SchemeCore.builtin
{
    class SchemeBuiltinAnd : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate(ref SchemeAST currentAST, SchemeEvaluator evaluator)
        {
            var list = lookupSymbolsFromEnv(ref currentAST, evaluator.currentEnvironment);
            Debug.Assert(list[0].GetType() == typeof(SchemeBuiltinAnd));

            if (list.Count < 3)
            {
                throw new SchemeWrongNumberOfArguments(String.Format("Too few arguments! and need at least 2 arguments. You specified: {0}", list.Count - 1));
            }

            try
            {
                foreach (SchemeBool value in list.GetRange(1, list.Count - 1))
                {
                    if ( value != SchemeTrue.instance )
                    {
                        return SchemeFalse.instance;
                    }
                }
            }
            catch (InvalidCastException)
            {
                throw new SchemeInvalidArgumentException("Builtin Function AND expects SchemeInteger or SchemeFloat as parameter. Got something else.");
            }

            return SchemeTrue.instance;
        }

        public override string ToString()
        {
            return "SchemeBuilitin and";
        }
    }
}
