 using System;
using System.Collections.Generic;
using System.Text;
using SchemeCore.helper;

namespace SchemeCore.objects
{
    class SchemeLambdaTemplate : SchemeLambda
    {

        private List<SchemeSymbol> _params = new List<SchemeSymbol>();
        private SchemeAST _implementation;
        public override ISchemeEnvironment _lambdaEnv { get; set; }

        public SchemeLambdaTemplate( SchemeAST ast, ISchemeEnvironment currentEnv )
        {
            if (ast.children.Count < 2)
            {
                throw new SchemeWrongNumberOfArguments(String.Format("Lambda expects exactly two arguments. You have given me: {0}", ast.children.Count));
            }

             
            //really not beautiful, but this is the price we pay for using the AST data structure
            if (ast.children[0].currentObject != SchemeVoid.instance) // this is false for lambdas with no arguments
            {
                _params.Add((SchemeSymbol)ast.children[0].currentObject);
                foreach (SchemeAST child in ast.children[0].children)
                {
                    _params.Add((SchemeSymbol)child.currentObject);
                }
            }
            _implementation = new SchemeAST();

            _lambdaEnv = currentEnv ;

            for (int i = 1; i < ast.children.Count; i++)
            {
                _implementation.children.Add((SchemeAST)ast.children[i].Clone());
                _implementation.children[_implementation.children.Count - 1].parent = ast;
            }


        }

        public override SchemeObject evaluate( ref SchemeAST currentAST, SchemeEvaluator evaluator )
        {
            if( currentAST.children.Count != _params.Count )
            {
                throw new SchemeWrongNumberOfArguments( String.Format( "Lambda expects exactly two arguments. You have given me: {0}", currentAST.children.Count ) );
            }

                 


                    var tmp = _lambdaEnv.getClonedEnv(evaluator.currentEnvironment); 
                    //_lambdaEnv.setParent(evaluator.currentEnvironment);
                       //evaluator.currentEnvironment = _lambdaEnv;


                    SchemeLambda lambda = new SchemeLambdaImpl(_implementation, _params, new SchemeEnvironment(tmp) );



            lambda.evaluate(ref currentAST, evaluator);
            return null;

        }

        public override string ToString()
        {
            return "Lambda";
        }


    }
}
