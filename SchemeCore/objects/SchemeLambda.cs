using System;
using System.Collections.Generic;

using System.Text;

using SchemeCore.helper;
namespace SchemeCore.objects
{
    class SchemeLambda : SchemeType, ISchemeFunction
    {
        private List<SchemeSymbol> _params = new List<SchemeSymbol>();
        private SchemeAST _implementation;
        private ISchemeEnvironment _lambdaEnv;

        public SchemeLambda( SchemeAST ast, ISchemeEnvironment currentEnv )
        {
            if( ast.children.Count < 2 )
            { 
                throw new SchemeWrongNumberOfArguments(String.Format("Lambda expects exactly two arguments. You have given me: {0}",ast.children.Count ));
            }

            _lambdaEnv = new SchemeEnvironment(currentEnv); ;
            //really not beautiful, but this is the price we pay for using the AST data structure
            if( ast.children[0].currentObject != SchemeVoid.instance ) // this is false for lambdas with no arguments
            {
                _params.Add( (SchemeSymbol) ast.children[0].currentObject );
                foreach( SchemeAST child in ast.children[0].children )
                {
                    _params.Add( (SchemeSymbol) child.currentObject );
                }
            }
            _implementation = new SchemeAST();

            

                for( int i = 1; i < ast.children.Count; i++ )
                {
                    _implementation.children.Add( (SchemeAST) ast.children[i].Clone() );
                    _implementation.children[_implementation.children.Count - 1].parent = ast;
                }


            }
           
        

        public SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != _params.Count )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Lambda expects exactly two arguments. You have given me: {0}", currentAST.children.Count ) );
            }

          if (evaluator.currentEnvironment != _lambdaEnv)
            {

                _lambdaEnv.setParent(evaluator.currentEnvironment);
                evaluator.currentEnvironment = _lambdaEnv;

            }
            else
            {
                int i = 0;
            }  


            for( int i = 0; i < _params.Count; i++ )
            {
                var child = currentAST.children[i];
                if( child.currentObject.GetType() == typeof( SchemeSymbol ) )
                {
                    var val = evaluator.currentEnvironment.parent().get( (SchemeSymbol) child.currentObject );

                    if( val == null )
                    {
                       
                        val = evaluator.currentEnvironment.get((SchemeSymbol)child.currentObject);

                        if (val == null)
                        {
                            throw new SchemeUndefinedSymbolException(String.Format("Undefined Symbol!: {0}", child.currentObject));
                        }
                    }
                    evaluator.currentEnvironment.set( _params[i], val );
                }
                else
                {
                    evaluator.currentEnvironment.set( _params[i], (SchemeType) child.currentObject );
                }
               
            }

            var clonedImplementation = (SchemeAST)_implementation.Clone();
            
            var oldParent = currentAST.parent;
            var index = currentAST.parent.children.IndexOf( currentAST );
            currentAST.parent.children.Remove(currentAST);


            for( int i = 0; i < clonedImplementation.children.Count; i++ )
            {
                currentAST.parent.children.Insert( i + index, clonedImplementation.children[i] );
                clonedImplementation.children[i].parent = oldParent;

                if( i == clonedImplementation.children.Count - 1 )   // the last one being added
                {
                  currentAST.parent.children[i + index].hasOwnEnviornment = true; // set flag for enviornment switch
                }
            }


            currentAST = currentAST.parent.children[index];
            
            return null ; //so ugly, but null means: to be evaluated again!
        }

        public override string ToString()
        {
            return "Lambda"; 
        }
    }
}
