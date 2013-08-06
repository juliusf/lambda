using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore.objects;
using SchemeCore.helper;

namespace SchemeCore.objects
{
    internal interface ISchemeFunction
    {
        SchemeObject evaluate( ref SchemeAST currentAST, ISchemeEnvironment env );
    }

    internal abstract class SchemeBuiltInFunction :SchemeType, ISchemeFunction
    {
        public SchemeObject evaluate( ref SchemeAST currentAST, ISchemeEnvironment env )
        {
            var methodObjects = lookupSymbolsFromEnv( ref currentAST, env );
            return evaluate( methodObjects.GetRange( 1, methodObjects.Count - 1 ), env);
        }
        internal abstract SchemeObject evaluate( List<SchemeObject> objects, ISchemeEnvironment env );
        public abstract override string ToString();
       
         private List<SchemeObject> lookupSymbolsFromEnv( ref SchemeAST currentAST, ISchemeEnvironment environment )
        {
            List<SchemeObject> ret = new List<SchemeObject>();
            ret.Add( environment.get( (SchemeSymbol) currentAST.currentObject ) );

            for( int i = 0; i < currentAST.children.Count; i++ )
            {
                if( currentAST.children[i].currentObject.GetType() == typeof( SchemeSymbol ) )
                {
                    var symbol = (SchemeSymbol) currentAST.children[i].currentObject;
                    ret.Add( environment.get( symbol ) );

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
                else
                {
                    ret.Add( currentAST.children[i].currentObject );
                }
            }
            return ret;
        }


       
    }
}
