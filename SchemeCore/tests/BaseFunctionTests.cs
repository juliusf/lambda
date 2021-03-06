﻿using System;
using System.Collections.Generic;

using System.Text;


using NUnit.Framework;
using SchemeCore.objects;
using SchemeCore;
using SchemeCore.builtin;

namespace SchemeCore.tests
{
    [TestFixture]
    public class BaseFunctionTests
    {
        [Test]
        public void schemeEnvironentTest()
        {
            var root = SchemeEnvironmentRoot.instance;

            var testSymbol = new SchemeSymbol( "foo" );

            SchemeEnvironment e1 = new SchemeEnvironment( root );
            SchemeEnvironment e2 = new SchemeEnvironment( e1 );

            root.set( testSymbol, new SchemeInteger( 1 ) );

            Assert.AreEqual( root.get( testSymbol ), new SchemeInteger ( 1 ) );
            Assert.AreEqual( e1.get( testSymbol ), new SchemeInteger( 1 ) );
            Assert.AreEqual( e2.get( testSymbol ), new SchemeInteger( 1 ) );

            var e1foo = e1.get( testSymbol );

            Assert.IsTrue( ( (SchemeInteger) e1foo ).value == 1 );

            e2.set( testSymbol, new SchemeInteger( 2 ) );
            root.set( testSymbol, new SchemeInteger( 4 ) );

            Assert.IsTrue( e2.get( testSymbol ) != root.get( testSymbol ) );

            //test correctness in in evaluator

            var reader = new SchemeReader();
            var evaluator = new SchemeEvaluator();

            var ast = reader.parseString( "(define x 1)" );
            evaluator.evaluate( ast );

            ast = reader.parseString( "x" );
            Assert.AreEqual( evaluator.evaluate( ast )[0], new SchemeInteger( 1 ) );

            ast = reader.parseString( "(define bar (lambda () (define x 23) (x)))" );
            evaluator.evaluate( ast );

            ast = reader.parseString( "(bar)" );
            Assert.AreEqual( evaluator.evaluate( ast )[1], new SchemeInteger( 23 ) );

            ast = reader.parseString( "x" );
            Assert.AreEqual( evaluator.evaluate( ast )[0], new SchemeInteger( 1 ) );

        }

        [Test]
        public void complexEnviornmentTest()
        {
            var root = SchemeEnvironmentRoot.instance;
            var reader = new SchemeReader();
            var evaluator = new SchemeEvaluator();

            var ast = reader.parseString( "(define foo (lambda (param) (bar param param))" );
            evaluator.evaluate( ast );
            Assert.AreEqual( root, evaluator.environment );
            //Assert.AreEqual( 1, evaluator.environment.getDict().Count );
            Assert.AreEqual( evaluator.environment.getDict().ContainsKey( "foo" ), true );
            Assert.AreEqual( evaluator.environment.getDict()["foo"] is  SchemeLambda, true  );

             ast = reader.parseString( "(define bar +) (foo 1)" );
             evaluator.evaluate( ast );

             //Assert.AreEqual( root, evaluator.environment );
             Assert.AreEqual( evaluator.environment.getDict()["bar"].GetType(), typeof( SchemeBuiltInPlus ) );
             Assert.AreEqual( true, ( (SchemeLambda) evaluator.environment.getDict()["foo"] )._lambdaEnv.has( new SchemeSymbol( "bar" ) ) );
             

        }

        [Test]
        public void SchemeEnviornmentTestExtended()        
        {
            var reader = new SchemeReader();
            var ast = reader.parseString( "(lambda () (define x 1))" );

        }

        [Test]
        public void SchemeASTTest()
        {
            SchemeAST root = new SchemeAST();
            SchemeAST ast = new SchemeAST( root,  SchemeNil.instance );

            Assert.IsTrue( ast.parent == root );

            SchemeAST child1 = new SchemeAST( ast, SchemeNil.instance );
            SchemeAST child2 = new SchemeAST( child1, SchemeTrue.instance );

            child1.children.Add( child2 );
            ast.children.Add( child1 );

            Assert.IsTrue( ast.children.Count == 1 );
            Assert.IsTrue( ast.children[0].parent == ast );
            Assert.IsTrue( ast.currentObject == SchemeNil.instance );
            Assert.IsTrue( ast.children[0].children.Count == 1 );
            Assert.IsTrue( ast.children[0].children[0] == child2 );
            Assert.IsTrue( ast.children[0].children[0].parent == child1 );
            Assert.IsTrue( ast.children[0].children[0].currentObject == SchemeTrue.instance );

            SchemeAST clone = (SchemeAST)ast.Clone();

            Assert.AreEqual( clone, ast );

           // Assert.AreEqual( clone, ast );

        }
        [Test]
        public void SchemeReaderTest()
        {
            var reader = new SchemeReader();
            var arr = reader.tokenize( "   \n\r\n\r\n\r\n \r\r\r\r  \r\r\r\n\n \n\n\n\n (define   \t\t\t  x \n\n\r\r\r   ( + \t 1 \t\t\t\t\t\t\t\t 3 ) \t\t\t     \t\t )" );
            var arrpos = reader.tokenizeWithPos( "   \n\r\n\r\n\r\n \r\r\r\r  \r\r\r\n\n \n\n\n\n (define   \t\t\t  x \n\n\r\r\r   ( + \t 1 \t\t\t\t\t\t\t\t 3 ) \t\t\t     \t\t )" );
            List<string> arr2 = new List<string>();
            arr2.Add( "(" );
            arr2.Add( "define" );
            arr2.Add( "x" );
            arr2.Add( "(" );
            arr2.Add( "+" );
            arr2.Add( "1" );
            arr2.Add( "3" );
            arr2.Add( ")" );          
            arr2.Add( ")" );

            CollectionAssert.AreEqual( arr, arr2 );

            string expression = "(+ 4 (- 2 1) (- (+ (- 2 2) 2) 9))";
            Assert.AreEqual( expression, reader.parseString( expression ).ToString() );
        }

        [Test]
        public void SchemeEvaluatorTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            SchemeAST ast = reader.parseString( "(+ 1 (+ 1 1) (+ 4 (+ 12 3))" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 22 ) );

            ast = reader.parseString( "(define x 13)" );
            eval.evaluate( ast );
            ast = reader.parseString( "x" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 13 ) );

            ast = reader.parseString( "1 2" ); 
        }

    }
}
