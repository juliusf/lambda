﻿using System;
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


            //really not beautiful, but this is the price we pay for using the AST data structure
            if( ast.children[0].currentObject != SchemeVoid.instance )
            {
                _params.Add( (SchemeSymbol) ast.children[0].currentObject );
                foreach( SchemeAST child in ast.children[0].children )
                {
                    _params.Add( (SchemeSymbol) child.currentObject );
                }
            }
            _implementation = (SchemeAST) (ast.children[1]).Clone();
        }

        public SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != _params.Count )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Lambda expects exactly two arguments. You have given me: {0}", currentAST.children.Count ) );
            }

            var newEnv = new SchemeEnvironment( evaluator.currentEnvironment );
            for( int i = 0; i < _params.Count; i++ )
            {
                var child = currentAST.children[i];
                if( child.currentObject.GetType() == typeof( SchemeSymbol ) )
                {
                    var val = evaluator.currentEnvironment.get( (SchemeSymbol) child.currentObject );

                    if( val == null )
                    {
                        throw new SchemeUndefinedSymbolException( String.Format( "Undefined Symbol!: {0}", child.currentObject ) );
                    }
                    newEnv.set( _params[i], val );
                }
                else
                {
                    newEnv.set( _params[i], (SchemeType) child.currentObject );
                }
               
            }

            var clonedImplementation = (SchemeAST)_implementation.Clone();
            var oldParent = currentAST.parent;
            var foo = currentAST.parent.children.IndexOf(currentAST);
            currentAST.parent.children.Remove(currentAST);
            currentAST.parent.children.Insert(foo, clonedImplementation);

          //  foreach (SchemeAST child in clonedImplementation.children)
          //  {
           //     child.parent = clonedImplementation;
           // }
            clonedImplementation.parent = oldParent;
            currentAST = clonedImplementation;
            
            

            
            
            

            if( foo == -1 )
            {
                int i = 1;
            }
           



            
            evaluator.currentEnvironment = newEnv;
            return null ; //so ugly, but null means: to be evaluated again!
        }

        public override string ToString()
        {
            return "Lambda"; 
        }
    }
}
