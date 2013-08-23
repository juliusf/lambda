using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore.objects;
using SchemeCore.helper;
using SchemeCore.builtin;

namespace SchemeCore.objects
{
    internal interface ISchemeFunction
    {
        SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator );
    }

    internal abstract class SchemeBuiltInFunction :SchemeType, ISchemeFunction
    {


      /*  public SchemeObject evaluate( ref SchemeAST currentAST, ISchemeEnvironment env )
        {
            this.currentAST = currentAST;
            this.environment = env;

            //syntax like define is a special case where we don't want the expresisons to be evaluated!
            if( handleSyntax() )
            {
                return SchemeVoid.instance;
            }
            else
            {
                var methodObjects = lookupSymbolsFromEnv();
                return evaluateInternal( methodObjects.GetRange( 1, methodObjects.Count - 1 ) );
            }
            
        }   */
        public abstract SchemeObject evaluate(ref SchemeAST currentAST, SchemeEvaluator evaluator );
        public abstract override string ToString();



        internal static List<SchemeObject> lookupSymbolsFromEnv( ref SchemeAST currentAST, ISchemeEnvironment environment )
        {
            List<SchemeObject> ret = new List<SchemeObject>();
            ret.Add( environment.get( (SchemeSymbol) currentAST.currentObject ) );
             // not nice, but it works
          /*  if( ret[0] == null ) // this holds true if the currentObjcet is NOT in the symbol table. Then it might either be an integer or a float and has not been redefined or the function is unknown
            {
                ret.RemoveAt( 0 );
                int intValue;
                var symbol = ( (SchemeSymbol) currentAST.currentObject ).value;
                if( int.TryParse( symbol, out intValue ) )
                {
                    ret.Add( new SchemeInteger( intValue ) );
                }
                else //TODO extend for floats
                {
                    throw new SchemeUndefinedSymbolException( String.Format( "Undefined Symbol: {0}", symbol ) );
                }
            }   */

            foreach (SchemeAST child in currentAST.children)
            {
                if( child.currentObject.GetType() == typeof( SchemeSymbol ) )
                {
                    var symbol = (SchemeSymbol) child.currentObject;
                    ret.Add( environment.get( symbol ) );

                    if( ret[ret.Count -1]== null )  //objcet is not in symbol list, check for integer and float!
                    {
                            throw new SchemeUndefinedSymbolException( String.Format( "Undefined Symbol: {0}", symbol.value ) );
                    }
                }
                else
                {
                    ret.Add( child.currentObject );
                }
            }
            return ret;
        }


       
    }
}
