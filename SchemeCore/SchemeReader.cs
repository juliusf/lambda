using System;
using System.Collections.Generic;

using System.Text;

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

                        var oldParent = currentParent;
                        currentParent = new SchemeAST( oldParent, makeSchemeObject( tokens[++i] ) ); //make new ast node and assign it to the parent. Also advance the counter!
                        oldParent.children.Add( currentParent );
                }
                else if( token == ")" )
                {
                    currentParent = currentParent.parent; //jump up one level
                }
                else
                {
                        currentParent.children.Add( new SchemeAST( currentParent, makeSchemeObject( token ) ) );
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

        public List<Token> tokenizeWithPos( string input )
        {
            input = input.Replace( "\r", " " );
            input = input.Replace( "\t", " " );

            var result = new List<Token>();
            string currentToken = "";
            int lnCnt = 0;
            int colcnt = 0;
            
            foreach( char c in input )
            {
                if( c == '\n' )
                {
                    // add to result here, if (currToken != "") ---
                    if( currentToken != "" )
                    {
                        appendToken( ref result, ref currentToken, lnCnt, colcnt );
                    }
                    lnCnt++;
                    colcnt = 0;
                    continue;
                }
                colcnt++;

               switch( c )
               { 
                   case '(':
                   case ')':
                       appendToken( ref result, ref currentToken, lnCnt, colcnt );
                       //result.Add(c.ToString());
                       break;
                   case ' ':
                       appendToken( ref result, ref currentToken, lnCnt, colcnt );
                       break;
                   default:
                       currentToken += c;
                       break;   
               }
           }
            appendToken( ref result, ref currentToken, lnCnt, colcnt );

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

        void appendToken( ref List<Token> result, ref string token, int numlines, int numcols )
        {
            if( token != "" )
            {
                var res = new Token();
                res.token = token;
                res.line = numlines;
                res.col = numcols;

                result.Add( res );
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
            else  
            {
                return new SchemeSymbol( token );
            }
        }
    }

    public  struct Token
    {
        public string token;
        public int line;
        public int col;
    }
}
