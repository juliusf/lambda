using System;
using System.Collections.Generic;

using System.Text;

using SchemeCore.objects;
 

namespace SchemeCore.builtin
{
    class SchemeBuiltInLambda : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            return new SchemeLambda( currentAST, evaluator.currentEnvironment );     // The current class is only a wrapper for the lambda keyword. This call creates the new SchemeLambda Object
        }

        public override string ToString()
        {
            return "SchemeBuiltin Lamda";
        }
    }
}
