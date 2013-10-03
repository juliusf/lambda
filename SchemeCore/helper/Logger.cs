using System;
using System.Collections.Generic;

using System.Text;


namespace SchemeCore.helper
{
    public class Logger
    {
        public static bool enableLogfile { get; set; }
        public static bool enableConsoleLog { get; set; }
        public static bool enableEventLog { get; set; }

        public  delegate void logWriteEventHandler( string text );
        public  delegate void logWriteLnEventHandler( string text );

        public static event logWriteEventHandler logWrite;
        public static event logWriteLnEventHandler logWriteLn;

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

            if( enableEventLog )
            {
                logWriteLn( input );
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

            if( enableEventLog )
            {
                logWrite( input );
            }
            
            }
        }
        }


