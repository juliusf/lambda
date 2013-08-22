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
            root.set( new SchemeSymbol( "define" ), new SchemeBuitInDefine() );
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
                
                if( currentAST.parent != null )
                {
                    updateParent( ref currentAST, evaluated );                //replace currentAST with result
                    currentAST = currentAST.parent; //ascend the tree again
                }

                

                if( currentAST.parent == null )
                {
                    if( currentAST.children.Count == 0 )
                    {   //always return atleas void!
                        currentAST.children.Add(new SchemeAST( currentAST, SchemeVoid.instance ));
                    }
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
            if(  newValue != SchemeVoid.instance )
            {
                var returnValue = new SchemeAST( currentAST.parent, newValue );
                currentAST.parent.children.Insert( postition, returnValue );
            }
           

        }

        private SchemeObject evaluateSchemeAST( SchemeAST ast, ISchemeEnvironment environment )
        {
            var func = getFunction( ref ast, environment );
            if( func != null )
            {
                return func.evaluate( ref ast, new SchemeEnvironment( environment ) );
            }
            else  
            {
                var type = getType(ref ast, environment);
                if( type != null )
                {
                    return type;
                }
                else
                {   
                    //it might be an expression which has to be evaluated directly:
                    
                    throw new SchemeNoSuchMethodException( String.Format( "{0} is no valid Scheme Method!", ast.currentObject.ToString() ) );
                }
            }
            
        }

        private ISchemeFunction getFunction( ref SchemeAST ast, ISchemeEnvironment environment )
        {
            SchemeObject ret = ast.currentObject;
            if ( ast.currentObject.GetType() == typeof(SchemeSymbol) )
            {
                ret = environment.get( (SchemeSymbol) ret );
            }  
            
           
            if( !(  ret is ISchemeFunction)  )
            {
                return null; 
            }

            return (ISchemeFunction) ret;

        }

        private SchemeType getType( ref SchemeAST ast, ISchemeEnvironment environment )
        {
            SchemeObject ret = ast.currentObject;
            if( ast.currentObject.GetType() == typeof( SchemeSymbol ) )
            {
                ret = environment.get( (SchemeSymbol) ret );
            }


            if( !(ret is SchemeType ) )
            {
                return null;
            }

            return (SchemeType) ret;
        }
    }
}

