using System;
using System.Collections.Generic;
using System.Text;
using SchemeCore.helper;

namespace SchemeCore.objects
{
    class SchemeLambdaImpl : SchemeLambda
    {
        private List<SchemeSymbol> _params = new List<SchemeSymbol>();
        private SchemeAST _implementation;
        public override ISchemeEnvironment _lambdaEnv { get; set; }

        public SchemeLambdaImpl( SchemeAST implementation, List<SchemeSymbol> _params, SchemeEnvironment env )
        {
            this._implementation = implementation;
            this._params = _params;
            this._lambdaEnv = env;

        }

        public SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            evaluator.currentEnvironment = _lambdaEnv;

            for( int i = 0; i < _params.Count; i++ )
            {
                var child = currentAST.children[i];
                if( child.currentObject.GetType() == typeof( SchemeSymbol ) )
                {
                    var val = evaluator.currentEnvironment.get( (SchemeSymbol) child.currentObject );

                    if( val == null )
                    {

                        val = evaluator.currentEnvironment.get( (SchemeSymbol) child.currentObject );

                        if( val == null )
                        {
                            throw new SchemeUndefinedSymbolException( String.Format( "Undefined Symbol!: {0}", child.currentObject ) );
                        }
                    }
                    evaluator.currentEnvironment.set( _params[i], val );
                }
                else
                {
                    evaluator.currentEnvironment.set( _params[i], (SchemeType) child.currentObject );
                }

            }

            var clonedImplementation = (SchemeAST) _implementation.Clone();

            var oldParent = currentAST.parent;
            var index = currentAST.parent.children.IndexOf( currentAST );
            currentAST.parent.children.Remove( currentAST );


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


            return null; //so ugly, but null means: to be evaluated again!
        }
    }
}
