using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using SchemeCore.objects;
using SchemeCore.builtin;

namespace SchemeCore.tests
{
    [TestFixture]
    public class SchemeBuiltinFunctionTest
    {
        [Test]
        public void schemeBuiltInPlusTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString( "(+ 32 8)"  );
            Assert.AreEqual( new SchemeInteger( 40 ), eval.evaluate( ast )[0] );


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
            Assert.AreEqual(eval.evaluate( ast )[0], new SchemeInteger(3));

            ast = reader.parseString( "(bar (+ 1 2) 2)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 5 ) );
            Console.WriteLine("begin definition: foo");
            ast = reader.parseString("(define bar (lambda (a) (if (= a 10) a (+ (bar (+ a 1)) 1 ))))");
            eval.evaluate(ast);
            Console.Out.WriteLine("begin call foo");
            ast = reader.parseString("(bar 1)");
            Assert.AreEqual(eval.evaluate(ast)[0], new SchemeInteger(19) );
            ast = reader.parseString( "(define foo (lambda (a) (if (= a 10) a (foo (+ a 1)))))" );
            eval.evaluate( ast );
            
            ast = reader.parseString( "(foo 1)"); 
        //    Assert.AreEqual( eval.evaluate( ast ), new SchemeInteger( 10 ) );



        }

        [Test]
        public void schemeBuiltInEqualsTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString( "(= 1 2)" );
            Assert.AreEqual( eval.evaluate( ast )[0], SchemeFalse.instance );

            ast = reader.parseString( "(= 1 1)" );
            Assert.AreEqual( eval.evaluate( ast )[0], SchemeTrue.instance );


        }
        [Test]
        public void schemeBuiltinIfTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString( "(if (= 1 1) 1 2)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger(1) );

            ast = reader.parseString( "(if (= 1 2) 1 2)" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger(2) );


        }

        [Test]
        public void schemeBuiltinModuloTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString( "(modulo 7 3" );
            Assert.AreEqual( eval.evaluate( ast )[0], new SchemeInteger( 1 ) );

        }
 

    }
}
