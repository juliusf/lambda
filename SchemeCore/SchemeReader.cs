using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using SchemeCore.objects;

namespace SchemeCore
{
    public class SchemeReader
    {
  

        public SchemeAST parseString( string input )
        {
            SchemeAST root = new SchemeAST();
            SchemeAST currentParent = root;

            var tokens = tokenize( input );

            for( int i = 0; i < tokens.Count; i++ )
            {
                string token = tokens[i];
                if( token == "(" )
                {
                    var j = 1 + i;
                    if( tokens[j] == ")" )   //peek if void
                    {
                        currentParent.children.Add(new SchemeAST( currentParent, SchemeVoid.instance )); 
                        i += 2;
                    }

                    if( root.currentObject != SchemeVoid.instance )
                    {
                        var oldParent = currentParent;
                        currentParent = new SchemeAST( oldParent, makeSchemeObject( tokens[++i] ) ); //make new ast node and assign it to the parent. Also advance the counter!
                        oldParent.children.Add( currentParent );
                    }
                    else // This guarantees that the first current Object really is the first function and not void
                    {
                        root.currentObject = makeSchemeObject( tokens[++i] );
                    }
                }
                else if( token == ")" )
                {
                    currentParent = currentParent.parent; //jump up one level
                }
                else
                {
                    if( root.currentObject != SchemeVoid.instance )
                    {
                        currentParent.children.Add( new SchemeAST( currentParent, makeSchemeObject( token ) ) );
                    }
                    else  // this is true for expressions which have to evaluated directly
                    {
                        root.currentObject = makeSchemeObject( token );
                    }
                }
            }
            return root;
        }


        public List<string> tokenize( string input )
        { 
           List<string> result = new List<string>();
           string currentToken = "";

           input = input.Replace( "\n", " " );
           input = input.Replace( "\r", " " );
           input = input.Replace( "\t", " " );

           input =  Regex.Replace(input,@"\s+"," "); //replaces multiple whitespace with one
           input = input.Trim(); //remove trailing and leading whitespace

           foreach( char c in input )
           {
               switch( c )
               { 
                   case '(':
                   case ')':
                       appendToken( ref result, ref currentToken );
                       result.Add(c.ToString());
                       break;
                   case ' ':
                       appendToken( ref result, ref currentToken );
                       break;
                   default:
                       currentToken += c;
                       break;   
               }
           }
           appendToken( ref result, ref currentToken );
           return result;

        }

        void appendToken( ref List<string> result, ref string token )
        {
            if( token != "" )
            {
                result.Add( token );
                token = "";

            }
        }

        SchemeObject makeSchemeObject( string token )
        {
            if( token == SchemeNil.instance.ToString() )
            {
                return SchemeNil.instance;
            }
            else if( token == SchemeFalse.instance.ToString() )
            {
                return SchemeFalse.instance;
            }
            else if( token == SchemeTrue.instance.ToString() )
            {
                return SchemeTrue.instance;
            }
            else   //TODO if token not in symbol-table, try to cast, create schemeInteger
            {
                return new SchemeSymbol( token );
            }
        }
    }
}
