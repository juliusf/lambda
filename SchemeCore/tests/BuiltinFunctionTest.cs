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

 

    }
}
