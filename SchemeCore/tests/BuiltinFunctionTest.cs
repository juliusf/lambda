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
            List<SchemeObject> list = new List<SchemeObject>();
            var object1 = new SchemeInteger( 3 );
            var object2 = new SchemeInteger( 5 );
            var object3 = new SchemeInteger( 32 );

            list.Add(object1);
            list.Add(object2);
            list.Add(object3);
            var function = new  SchemeBuiltInPlus();

            Assert.AreEqual( new SchemeInteger( 40 ), function.evaluate( list, SchemeEnvironmentRoot.instance ) );


        }

    }
}
