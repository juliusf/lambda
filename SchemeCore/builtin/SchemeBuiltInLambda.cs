using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchemeCore.objects;
 

namespace SchemeCore.builtin
{
    class SchemeBuiltInLambda : SchemeBuiltInFunction
    {
        public override SchemeObject evaluate( ref SchemeAST currentAST, ISchemeEnvironment env )
        {
            return new SchemeLambda( currentAST );
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
