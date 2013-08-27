using SchemeCore.objects;
using SchemeCore.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.builtin
{
    class SchemeIf : SchemeBuiltInFunction
    {
        private SchemeAST _condition;
        private SchemeAST _trueBranch;
        private SchemeAST _falseBranch;

        public SchemeIf( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count < 2 || currentAST.children.Count > 3 )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "The If-statement expects 2 or 3 arguments (condition, branch1, branch2). You provided {0}", currentAST.children.Count - 1 ) );
            }

            _condition = currentAST.children[0];
            _trueBranch = currentAST.children[1];

            if( currentAST.children.Count == 3 )
            {
                _falseBranch = currentAST.children[2];
            }
        }
        
        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {

            

            
            if( _condition.currentObject == SchemeTrue.instance )
            {
             var old_parent = currentAST.parent;
            _trueBranch.parent= currentAST.parent;
            
            int postition = currentAST.parent.children.IndexOf( currentAST );
            currentAST.parent.children.Remove( currentAST );
            currentAST.parent.children.Add( _trueBranch );



            currentAST = _trueBranch;
            return null;
            }

            if( _condition.currentObject == SchemeFalse.instance )
            {
                if( _falseBranch != null )
                {
                    var old_parent = currentAST.parent;
                    _falseBranch.parent = currentAST.parent;
                    //update every child's parent in falseBranch
        //            foreach (SchemeAST child in _falseBranch.children)
        //            {
        //                child.parent = _falseBranch;  
        //            }
                    int postition = currentAST.parent.children.IndexOf( currentAST );
                    currentAST.parent.children.Remove( currentAST );
                    currentAST.parent.children.Add( currentAST.children[2] );



                    currentAST = currentAST.children[2];
                    return null;
                }
                else
                {
                    return SchemeVoid.instance;
                }
            }

            throw new SchemeInvalidArgumentException(String.Format("Invalid Argument in If-condition: Your condtion has to evalue to either #t or #f, but it evaluated to {0}", _condition.ToString()));
        }

        public override string ToString()
        {
            return "if"; 
        }
    }
}
