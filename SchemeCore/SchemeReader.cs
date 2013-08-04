﻿using System;
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