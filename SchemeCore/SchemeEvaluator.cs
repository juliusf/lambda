using System;
using System.Collections.Generic;
using SchemeCore.objects;
using SchemeCore;
using SchemeCore.helper;
using SchemeCore.builtin;


namespace SchemeCore
{
    public class SchemeEvaluator
    {
        private SchemeEnvironmentRoot root;
        public SchemeEvaluator()
        {
            root = SchemeEnvironmentRoot.instance;

            //instanciate the builtin Functions:
            root.set( new SchemeSymbol( "+" ), new SchemeBuiltInPlus() );
        }

        public SchemeObject evaluate( SchemeAST AST )
        {

            return evaluateHelper( ref AST, root );
        }

        private SchemeObject evaluateHelper( ref SchemeAST currentAST, ISchemeEnvironment environment ) // TODO: Add SchemeEnvironment
        {

            while( true )
            {
                if( updateToNextLevelChild( ref currentAST ) ) //this updates currentAST until the first AST object is found which contains leaf objects only
                {
                    continue;
                }

                var evaluated = evaluateSchemeAST( currentAST, environment ); //evaluate the expression
                updateParent( ref currentAST, evaluated );                //replace currentAST with result


                currentAST = currentAST.parent; //ascend the tree again

                if( currentAST.parent == null )
                {
                    break;
                }
            }
            return currentAST.children[0].currentObject;
        }


        private bool updateToNextLevelChild( ref SchemeAST currentAst )
        {
            foreach( SchemeAST child in currentAst.children )
            {
                if( child.children.Count != 0 )
                {
                    currentAst = child;
                    return true;
                }
            }
            return false;
        }

        private void updateParent( ref SchemeAST currentAST, SchemeObject newValue )
        {
            int postition = currentAST.parent.children.IndexOf( currentAST );
            currentAST.parent.children.Remove( currentAST );
            var returnValue = new SchemeAST( currentAST.parent, newValue );
            currentAST.parent.children.Insert( postition, returnValue );
        }

        private SchemeObject evaluateSchemeAST( SchemeAST ast, ISchemeEnvironment environment )
        {

            var func = getFunction( ref ast, environment );


            return func.evaluate( ref ast, new SchemeEnvironment( environment ) );
        }

        private ISchemeFunction getFunction( ref SchemeAST ast, ISchemeEnvironment environment )
        {
            SchemeObject ret = ast.currentObject;
            if ( ast.currentObject.GetType() == typeof(SchemeSymbol) )
            {
                ret = environment.get( (SchemeSymbol) ret );
            }
            
            if( !( ret is ISchemeFunction ) )
            {
                throw new SchemeNoSuchMethodException( String.Format( "{0} is no valid Scheme Method!", ast.currentObject.ToString() ) );
            }

            return (ISchemeFunction) ret;

        }
    }
}

