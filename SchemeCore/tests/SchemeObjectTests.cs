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
    }
}
