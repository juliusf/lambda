using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore;
namespace LambdaCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = new SchemeCore.tests.BaseFunctionTests();
            foo.SchemeASTTest();


        }
    }
}
