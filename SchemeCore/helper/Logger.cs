using System;
using System.Collections.Generic;

using System.Text;


namespace SchemeCore.helper
{
    public class Logger
    {
        public static bool enableLogfile { get; set; }
        public static bool enableConsoleLog { get; set; }
        
        public static void writeLine( string input )
        {
            if( enableLogfile )
            {
              System.IO.StreamWriter file = new System.IO.StreamWriter("log.txt", true);
              file.WriteLine(input);
              file.Close();
            }

            if (enableConsoleLog)
            {
                Console.Out.WriteLine(input);
            }
            
            }

        public static void write( string input )
        {
          if( enableLogfile )
            {
              System.IO.StreamWriter file = new System.IO.StreamWriter("log.txt", true);
              file.Write(input);
              file.Close();
            }

            if (enableConsoleLog)
            {
                Console.Out.Write(input);
            }
            
            }
        }
        }


