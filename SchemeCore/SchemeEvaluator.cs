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
            root.set( new SchemeSymbol( "lambda" ), new SchemeBuiltInLambda() );
            root.set( new SchemeSymbol( "=" ) , new SchemeBuiltinEquals() );
            root.set( new SchemeSymbol( "if" ), new SchemeBuiltInIf() );
            root.set( new SchemeSymbol( "modulo" ), new ScehemBuiltinModulo() );
            root.set( new SchemeSymbol( "cons" ), new SchemeBuiltinCons() );
            root.set( new SchemeSymbol( "cdr" ), new SchemeBuiltinCdr() );
            root.set( new SchemeSymbol( "car" ), new SchemeBuiltinCar() );

        }
        
        public void loadSchemeLibrary( string path )
        {
            System.IO.StreamReader file = new System.IO.StreamReader( path );
            string libraryString = file.ReadToEnd();
            file.Close();

            var reader = new SchemeReader();
            var ast = reader.parseString( libraryString );

            evaluate( ast );
        }

        public List<SchemeObject> evaluate( SchemeAST AST )
        {
            if( currentEnvironment == null )
            {
                currentEnvironment = root;
            }
            return evaluate( ref AST);
        }

        internal ISchemeEnvironment currentEnvironment  { get; set; } // this is necessary because the ref keyword does not allow polymorphism
        internal  List<SchemeObject> evaluate( ref SchemeAST currentAST) 
        {
           
            var returnValue = new List<SchemeObject>();
            if( currentAST.currentObject == SchemeVoid.instance )
            {
               for( int i = 0; i < currentAST.children.Count; i++ )
                {
                    var ast = (SchemeAST)currentAST.children[i] ;
                    int instructionCount = 0; 
                   while( true )
                    {

                       Logger.writeLine(String.Format("Instruction: {0}", instructionCount ++));
                       if( Logger.enableConsoleLog || Logger.enableLogfile )
                       {
                           ast.createTreeOutput();
                       }
                           if( updateToNextLevelChild( ref ast, this.currentEnvironment ) ) //this updates currentAST until the first AST object is found which contains leaf objects only
                        {
                            continue;
                        }
                        var evaluated = evaluateSchemeAST( ref ast, this.currentEnvironment ); //evaluate the expression

                        if( evaluated == null)
                        {
                           continue;
                        }


                        updateParent( ref ast, evaluated );                //replace currentAST with result

                        if( ast.parent.currentObject != SchemeVoid.instance )
                        {
                                ast = ast.parent; //ascend the tree again
                        }
                        else
                        {
                          returnValue.Add( evaluated );
                          break;
                        }
                    }
                }
            }
            return returnValue;
        }


        private bool updateToNextLevelChild( ref SchemeAST currentAst , ISchemeEnvironment environment)
        {
            Type type = currentAst.currentObject.GetType();

            if( type == typeof( SchemeSymbol ) )
            {
                SchemeType t = environment.get( (SchemeSymbol) currentAst.currentObject );
                if( t == null )
                {
                    throw new SchemeUndefinedSymbolException( String.Format( "Undefined Symbol: {0}", currentAst.currentObject.ToString() ), currentAst.fileName, currentAst.sourceLength, currentAst.sourceOffset  );
                }
                Type obj =t.GetType();
                if( obj == typeof( SchemeBuiltInLambda ) ) // or typeof if. this is needed for parts which should not be evaluated.
                {
                    return false;
                }
                if( obj == typeof( SchemeBuiltInIf ) )
                {
                    var tmpRoot = new SchemeAST();
                    var condition = currentAst.children[0];
                    var oldParent = condition.parent;

                    tmpRoot.children.Add( condition );
                    condition.parent = tmpRoot;
                    var evaluated = evaluate( ref tmpRoot );    //evaluate the if condition
                    tmpRoot = new SchemeAST(  currentAst, evaluated[0] );
                    currentAst.children[0] = tmpRoot;

                    return false;
                }
            }
            
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
         /*   if (currentAST.currentObject.GetType() == typeof(SchemeLambda))
            {
                //  foreach (SchemeType obj in currrentEn )   
                foreach (string key in currentEnvironment.getDict().Keys)
                {
                    currentEnvironment.parent().set(new SchemeSymbol(key), currentEnvironment.getDict()[key]);
                }
            } */
            if (currentAST.hasOwnEnviornment)// && currentEnvironment.parent() != null )
            {
                if ( newValue is SchemeLambda )
                {
                    foreach (string key in currentEnvironment.getDict().Keys)
                    {
                        currentEnvironment.parent().set(new SchemeSymbol(key), currentEnvironment.getDict()[key]);
                    }  
                }
                currentEnvironment = currentEnvironment.parent();
            }

            int postition = currentAST.parent.children.IndexOf( currentAST );
            currentAST.parent.children.Remove( currentAST );
           // if(  newValue != SchemeVoid.instance )
          //  {
                var returnValue = new SchemeAST( currentAST.parent, newValue );
                currentAST.parent.children.Insert( postition, returnValue );
          //  }

               
        }

        private SchemeObject evaluateSchemeAST( ref SchemeAST ast, ISchemeEnvironment environment )
        {
           
            
            var func = getFunction( ref ast, environment );
            if( func != null )
            {
                return func.evaluate( ref ast, this );
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

