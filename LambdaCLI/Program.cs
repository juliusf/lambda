﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore;
using SchemeCore.objects;
using SchemeCore.tests;
namespace LambdaCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            var foo = new SchemeCore.tests.BaseFunctionTests();
            foo.SchemeReaderTest();

            while( true )
            {
                Console.Write( ">" ); 
                var ast = reader.parseString( Console.ReadLine() );
                try
                {
                    Console.WriteLine( eval.evaluate( ast ).ToString() );
                }
                catch( SchemeCore.helper.SchemeException e)
                { 
                Console.WriteLine(e.Message); 
                }
            }

        }
    }
}
