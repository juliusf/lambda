using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LambdaGUI
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new CodeEditorForm() );
        }
    }
}
