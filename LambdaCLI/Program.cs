using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore;
using SchemeCore.objects;
namespace LambdaCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SchemeReader reader = new SchemeReader();
            SchemeEvaluator eval = new SchemeEvaluator();

            SchemeAST ast = reader.parseString( "(+ 1 (+ 1 1) (+ 4 (+ 12 3))" );
            Console.WriteLine(eval.evaluate( ast ).ToString());
            

        }
    }
}
