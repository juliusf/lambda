using System;
using System.Collections.Generic;

using System.Text;


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

            var ast = reader.parseString("(+ 32 8)");
            Assert.AreEqual(new SchemeInteger(40), eval.evaluate(ast)[0]);


        }



        [Test]
        public void schemeBuiltInEqualsTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString("(= 1 2)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeFalse.instance);

            ast = reader.parseString("(= 1 1)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeTrue.instance);


        }
        [Test]
        public void schemeBuiltInGreaterTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString("(> 1 2)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeFalse.instance);

            ast = reader.parseString("(> 2 1)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeTrue.instance);

            ast = reader.parseString("(> 5 4 3 2 1)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeTrue.instance);

            ast = reader.parseString("(> 10 6 8 2 1)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeFalse.instance);

        }

        [Test]
        public void schemeBuiltInSmallerTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString("(< 1 2)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeTrue.instance);

            ast = reader.parseString("(< 2 1)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeFalse.instance);

            ast = reader.parseString("(< 1 2 3 4 5)");
            Assert.AreEqual(eval.evaluate(ast)[0], SchemeTrue.instance);

        }
        [Test]
        public void schemeBuiltinIfTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString("(if (= 1 1) 1 2)");
            Assert.AreEqual(eval.evaluate(ast)[0], new SchemeInteger(1));

            ast = reader.parseString("(if (= 1 2) 1 2)");
            Assert.AreEqual(eval.evaluate(ast)[0], new SchemeInteger(2));


        }

        [Test]
        public void schemeBuiltinModuloTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString("(modulo 7 3");
            Assert.AreEqual(eval.evaluate(ast)[0], new SchemeInteger(1));

        }
        [Test]
        public void schemeBuiltinConsTest()
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var ast = reader.parseString("(car (cons 10 11))");
            Assert.AreEqual(eval.evaluate(ast)[0], new SchemeInteger(10));

            ast = reader.parseString("(cdr (cons 10 11))");
            Assert.AreEqual(eval.evaluate(ast)[0], new SchemeInteger(11));

        }



    }
}
