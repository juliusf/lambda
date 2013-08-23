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

 

    }
}
