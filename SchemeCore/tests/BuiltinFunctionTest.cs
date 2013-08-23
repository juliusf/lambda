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
            Assert.AreEqual( new SchemeInteger( 40 ), eval.evaluate( ast ) );


        }

        [Test]
        public void schemeBuiltInLambdaTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString( "(define add (lambda (num1 num2)(+ num1 num2)))" );
            eval.evaluate( ast );
            ast = reader.parseString( "(add 1 2)" );

            Assert.AreEqual( eval.evaluate( ast ), new SchemeInteger( 3 ) );


        }

 

    }
}
