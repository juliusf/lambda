using System;
using System.Collections.Generic;

using System.Text;

using SchemeCore.helper;
using System.Diagnostics;
namespace SchemeCore.objects
{
    class SchemeLambda : SchemeType, ISchemeFunction
    {
        private List<SchemeSymbol> _params = new List<SchemeSymbol>();
        private SchemeAST _implementation;
        //public ISchemeEnvironment _lambdaEnv;

        public virtual ISchemeEnvironment _lambdaEnv { get; set; }

        public SchemeLambda()
        { 
        }

        public  SchemeLambda( SchemeAST ast, ISchemeEnvironment currentEnv )
        {
            Debug.Assert( false, "Should never be called!" );
        }

        


        public virtual SchemeObject evaluate(ref SchemeAST currentAST, SchemeEvaluator evaluator)
        {
            Debug.Assert( false, "Should never be called!" );
            return null; 
        }

        public override string ToString()
        {
            return "Lambda";
        }
    }
}
