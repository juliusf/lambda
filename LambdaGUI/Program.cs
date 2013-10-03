using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SchemeCore.helper;

namespace LambdaGUI
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Logger.enableConsoleLog = true;
            Logger.enableEventLog = true;
            Application.Run( new CodeEditorForm() );
        }
    }
}
