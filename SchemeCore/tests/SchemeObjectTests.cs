using System;
using System.Collections.Generic;

using System.Text;


using NUnit.Framework;
using SchemeCore.objects;

namespace SchemeCore.tests
{
    [TestFixture]
    public class SchemeObjectTests
    {
        [Test]
        public void schemeObjectTest()
        {
            var object1 = new SchemeSymbol( "foo" );
            var object2 = new SchemeSymbol( "foo" );
            var object3 = new SchemeSymbol( "bar" );

            Assert.AreEqual( true, object1.Equals( object2 ) );
            Assert.AreEqual( true, object2.Equals( object1 ) );
            Assert.AreEqual( false, object1.Equals( object3 ) );
            //Assert.AreSame( object1, object2 );
        }


        [Test]
        public void schemeListTest()
        {

            SchemeList l2 = new SchemeList( new SchemeInteger( 2 ) );
            SchemeList l1 = new SchemeList( new SchemeInteger( 5 ), l2 );

            Assert.AreEqual( true, l2.cdr.Equals( SchemeNil.instance ) );
            Assert.AreEqual( l1.cdr, l2 );
            Assert.AreEqual( l2.car, new SchemeInteger( 2 ) );
            Assert.AreEqual( l1.car, new SchemeInteger( 5 ) );

            Assert.AreEqual( "(5 (2))", l1.ToString() );

            SchemeList l3 = new SchemeList( new SchemeInteger( 9 ), new SchemeInteger( 1 ) );
            Assert.AreEqual( l3.ToString(), "(9 . 1)" );
        }

        [Test]
        public void nilSingletonTest()
        {
            SchemeNil foo = SchemeNil.instance;
            SchemeNil bar = SchemeNil.instance;

            Assert.AreEqual( foo, bar );

            Assert.AreEqual( foo.ToString(), "()" );
        }


        [Test]
        public void schemeBuiltInLambdaTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString( "(define add (lambda (num1 num2)(+ num1 num2)))" );
            eval.evaluate( ast );
            ast = reader.parseString( "(add 1 2)" );

            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 3 ) );

            ast = reader.parseString( "(define foo (lambda () (+ 1 2))) " );
            eval.evaluate( ast );
            ast = reader.parseString( "(foo)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 3 ) );

            ast = reader.parseString( "(define bar (lambda (a b) (add a b))" );
            eval.evaluate( ast );

            ast = reader.parseString( "(bar 1 2)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 3 ) );

            ast = reader.parseString( "(bar (+ 1 2) 2)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 5 ) );

            ast = reader.parseString( "(define bar (lambda (a) (if (= a 10) a (+ (bar (+ a 1)) 1 ))))" );
            eval.evaluate( ast );

            ast = reader.parseString( "(bar 1)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 19 ) );
            ast = reader.parseString( "(define foo (lambda (a) (if (= a 10) a (foo (+ a 1)))))" );
            eval.evaluate( ast );

            ast = reader.parseString( "(foo 1)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 10 ) );

        }

        [Test]
        public void schemeHigherOrderFunctionTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString( "(define scons (lambda (x y) (lambda (m) (m x y))))" );
            eval.evaluate( ast );
            ast = reader.parseString( "(define scar (lambda (z) (z (lambda (p q) p))))" );
            eval.evaluate( ast );
            ast = reader.parseString( "(scar (scons 10 11))" );

            Assert.AreEqual( new SchemeInteger( 10 ), eval.evaluate( ast )[0] );

            //Double evaluation Bug
            ast = reader.parseString( "(scar (scons 10 11))" );

            Assert.AreEqual( new SchemeInteger( 10 ), eval.evaluate( ast )[0] );

        }
    }
}
