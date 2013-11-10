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

        [Test]
        public void schemeSetTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var defineSingletonSet = reader.parseString( "(define singletonSet (lambda (x) (lambda (y) (= y x))))" );
            var defineContains = reader.parseString( "(define contains (lambda (set_ y) (set_ y)))" );
            var defineTestSets = reader.parseString( "(define s1 (singletonSet 1)) (define s2 (singletonSet 2))" );
            /*        "(define s1 (singletonSet 1)) " +
                    "(define s2 (singletonSet 2)) ";  +
                    "(define s3 (lambda (x) (and (>= x 5) (<= x 15))))" +
                    "(define s4 (lambda (x) (and (<= x -5) (>= x -15))))" +
                  ")");  */

            var test1 = reader.parseString("(contains s1 1)");
            var test2 = reader.parseString("(contains s2 2)");
            var test3 = reader.parseString("(contains s3 5)");
            var test4 = reader.parseString("(contains s4 -5)");
            var test5 = reader.parseString("(contains s4 -22)");

            eval.evaluate( defineSingletonSet );
            eval.evaluate( defineContains );
            eval.evaluate( defineTestSets ); 

            Assert.AreEqual( SchemeTrue.instance, eval.evaluate( test1 )[0] );
            Assert.AreEqual( SchemeTrue.instance, eval.evaluate( test2 )[0] );
            //Assert.AreEqual( SchemeTrue.instance, eval.evaluate( test3 )[0] );
            //Assert.AreEqual( SchemeTrue.instance, eval.evaluate( test4 ) );
            //Assert.AreEqual( SchemeFalse.instance, eval.evaluate( test5 ) );


        }
    }
}
