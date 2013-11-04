using SchemeCore;
using SchemeCore.objects;
using System;
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
            bar.complexEnviornmentTest();
            bar.schemeEnvironentTest();
            
            var foo = new SchemeCore.tests.SchemeObjectTests();
            //foo.schemeSetTest();
            
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
