using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore;
using SchemeCore.objects;
using SchemeCore.tests;
using System.IO;
namespace LambdaCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var bar = new SchemeCore.tests.BaseFunctionTests();
            bar.SchemeEvaluatorTest();
            bar.SchemeASTTest();
            var foo = new SchemeCore.tests.SchemeBuiltinFunctionTest();
            foo.schemeBuiltInLambdaTest();
            foo.schemeBuiltinIfTest();
            foo.schemeBuiltinModuloTest();

            var gcd = reader.parseString( "(define gcd (lambda (a b) (if (= b 0) a (gcd b (modulo a b)))))" );
            eval.evaluate( gcd );

            var recTest = reader.parseString( "(define foo (lambda (a) (if (= a 10) a (foo (+ a 1)))))" );

            eval.evaluate( recTest );

            SchemeCore.helper.Logger.enableConsoleLog = true;
            
            while( true )
            {
                Console.Write( ">" ); 
                var ast = reader.parseString( Console.ReadLine() );
                try
                {
                    var result = eval.evaluate( ast );
                    foreach( SchemeObject res in result)
                    {
                        Console.WriteLine( res );
                    }
                    
                }
                catch( SchemeCore.helper.SchemeException e)
                { 
                Console.WriteLine(e.Message);
                }
            }

        }
    }
}
