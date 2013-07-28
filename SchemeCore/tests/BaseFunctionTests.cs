using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using SchemeCore.objects;
using SchemeCore;

namespace SchemeCore.tests
{
    [TestFixture]
    public class BaseFunctionTests
    {
        [Test]
        public void schemeEnvironentTest()
        {
            var root = SchemeEnvironmentRoot.instance;

            var testSymbol = new SchemeSymbol( "foo" );

            SchemeEnvironment e1 = new SchemeEnvironment( root );
            SchemeEnvironment e2 = new SchemeEnvironment( e1 );

            root.set( testSymbol, new SchemeInteger( 1 ) );

            Assert.AreEqual( root.get( testSymbol ), new SchemeInteger ( 1 ) );
            Assert.AreEqual( e1.get( testSymbol ), new SchemeInteger( 1 ) );
            Assert.AreEqual( e2.get( testSymbol ), new SchemeInteger( 1 ) );

            var e1foo = e1.get( testSymbol );

            Assert.True( ( (SchemeInteger) e1foo ).value == 1 );

            e2.set( testSymbol, new SchemeInteger( 2 ) );
            root.set( testSymbol, new SchemeInteger( 4 ) );

            Assert.True( e2.get( testSymbol ) != root.get( testSymbol ) );

        }

        [Test]
        public void SchemeASTTest()
        {
            SchemeAST root = new SchemeAST();
            SchemeAST ast = new SchemeAST( root,  SchemeNil.instance );

            Assert.True( ast.parent == root );

            SchemeAST child1 = new SchemeAST( ast, SchemeNil.instance );
            SchemeAST child2 = new SchemeAST( child1, SchemeTrue.instance );

            child1.children.Add( child2 );
            ast.children.Add( child1 );

            Assert.True( ast.children.Count == 1 );
            Assert.True( ast.children[0].parent == ast );
            Assert.True( ast.currentObject == SchemeNil.instance );
            Assert.True( ast.children[0].children.Count == 1 );
            Assert.True( ast.children[0].children[0] == child2 );
            Assert.True( ast.children[0].children[0].parent == child1 );
            Assert.True( ast.children[0].children[0].currentObject == SchemeTrue.instance ); 

        }
    }
}
