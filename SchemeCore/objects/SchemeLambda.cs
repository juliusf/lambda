using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchemeCore.helper;
namespace SchemeCore.objects
{
    class SchemeLambda : SchemeType, ISchemeFunction
    {
        private List<SchemeSymbol> _params = new List<SchemeSymbol>();
        private SchemeAST _implementation;

        public SchemeLambda( SchemeAST ast )
        {
            if( ast.children.Count != 2 )
            { 
                throw new SchemeWrongNumberOfArguments(String.Format("Lambda expects exactly two arguments. You have given me: {0}",ast.children.Count ));
            }
            //TODO: Check if params is not empty. --> void functions

            //really not beautiful, but this is the price we pay by using the AST data structure
            _params.Add( (SchemeSymbol) ast.children[0].currentObject );
            foreach( SchemeAST child in ast.children[0].children )
            {
                _params.Add( (SchemeSymbol) child.currentObject );
            }

            _implementation = ast.children[1];
        }
        
        public SchemeObject evaluate( ref SchemeAST currentAST, ISchemeEnvironment env )
        {
            if( currentAST.children.Count != _params.Count )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Lambda expects exactly two arguments. You have given me: {0}", currentAST.children.Count ) );
            }

            SchemeEnvironment newEnv = new SchemeEnvironment( env );
            for( int i = 0; i < _params.Count; i++ )
            {
                var child = currentAST.children[i];
                if (child.currentObject.GetType() == typeof(SchemeSymbol) )
                {
                    var val = env.get( (SchemeSymbol) child.currentObject );

                    if (val == null)
                    {
                        throw new SchemeUndefinedSymbolException( String.Format( "Undefined Symbol!: {0}", child.currentObject ) ); 
                    }
                    newEnv.set( _params[i], val );
                } 
            }

            var evaluator = new SchemeEvaluator();
            var old_parent = _implementation.parent;
            _implementation.parent = null;
            var result = evaluator.evaluate( ref _implementation, newEnv );
            return result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
