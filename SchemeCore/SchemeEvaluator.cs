using System;

using SchemeCore.objects;
using SchemeCore;
using SchemeCore.helper;


namespace SchemeCore
{
	public class SchemeEvaluator
	{
        private SchemeEnvironmentRoot root;
		public SchemeEvaluator ()
		{
            root = SchemeEnvironmentRoot.instance;
		}

		public SchemeObject evaluate(SchemeAST AST)
		{

		return evaluateHelper(ref AST, root);
		}

		private SchemeObject evaluateHelper(ref SchemeAST currentAST, ISchemeEnvironment environment) // TODO: Add SchemeEnvironment
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
					if (child.children.Count != 0) 
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
			/*SchemeObject retValue = SchemeNil.instance;
			
            if (ast.currentObject.Equals(new SchemeSymbol ("+"))) { //TODO: overload SchemeSymbol ==
				int sum = 0;
				foreach (SchemeAST parameter in ast.children) {
					String sumString =  (((SchemeSymbol)parameter.currentObject).value);
                    int icast;
                    int.TryParse(sumString, out icast);
                    sum += icast;

				}
				retValue = new SchemeSymbol (sum.ToString());
			}
			return retValue; */


            if( ast.currentObject.GetType() == typeof( SchemeSymbol ) )
            {
                ast.currentObject = environment.get( (SchemeSymbol) ast.currentObject );
            }

            if( ast.currentObject.GetType() != typeof( ISchemeFunction ) )
            {
                throw new SchemeNoSuchMethodException( String.Format( "{0} is no valid Scheme Method!", ast.currentObject.ToString() ) );
            }

            return null;
		}

        private SchemeObject[] lookupSymbolsFromEnv( ref SchemeAST currentAST, ISchemeEnvironment environment )
        {
            SchemeObject[] ret = new SchemeObject[currentAST.children.Count + 1];
            ret[0] = currentAST.currentObject;

            for(int i=0; i<currentAST.children.Count;i++)
            {
                if( currentAST.children[i].currentObject.GetType() == typeof( SchemeSymbol ) )
                {
                  var symbol = (SchemeSymbol)currentAST.children[i].currentObject;
                  ret[i + 1] = environment.get( symbol  );

                    if( ret[i + 1] == null )  //objcet is not in symbol list, check for integer and float!
                    {
                        int intValue;

                        if( int.TryParse( symbol.value, out intValue ) )
                        {
                            ret[i + 1] = new SchemeInteger( intValue );
                        }
                        else //TODO extend for floats
                        {
                            throw new SchemeUndefinedSymbolException( String.Format( "Undefined Symbol: {0}", symbol.value ) );
                        }
                    }
                }



            }


            return null; 
        }

	}
}

