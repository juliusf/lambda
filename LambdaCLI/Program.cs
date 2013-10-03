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
            bar.SchemeReaderTest();
            bar.SchemeASTTest();
            var foo = new SchemeCore.tests.SchemeBuiltinFunctionTest();
            //foo.schemeBuiltInLambdaTest();
            //foo.schemeBuiltinIfTest();
            //foo.schemeBuiltinModuloTest();
        
            if( System.Diagnostics.Debugger.IsAttached )
            {
                SchemeCore.helper.Logger.enableConsoleLog = true;
                
            }

            //SchemeCore.helper.Logger.enableLogfile = true;
            //load the std
            
            eval.loadSchemeLibrary( "std.sch" );
            
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
