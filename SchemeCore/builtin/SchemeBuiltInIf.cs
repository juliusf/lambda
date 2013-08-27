using SchemeCore.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.builtin
{
    class SchemeBuiltInIf  : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            var _if = new SchemeIf( ref currentAST, evaluator );
            return _if.evaluate( ref currentAST, evaluator );
        }

        public override string ToString()
        {
            return "Scheme If"; 
        }
    }
}
