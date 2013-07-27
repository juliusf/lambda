using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using SchemeCore.objects;

namespace SchemeCore.tests
{   
    [TestFixture]
    public class SchemeSymbolTests
    {
        [Test]
        public void Equals()
        {
            var object1 = new SchemeSymbol("foo");
            var object2 = new SchemeSymbol("foo");
            var object3 = new SchemeSymbol("bar");

            Assert.AreEqual( true, object1.Equals( object2 ) );
            Assert.AreEqual( true, object2.Equals( object1 ) );
            Assert.AreEqual( false, object1.Equals( object3 ) );
        }
    }
}
