using System;
using System.Collections.Generic;

using System.Text;


namespace SchemeCore.objects
{
    class SchemeVoid : SchemeType
    {
        private static SchemeVoid _instance = new SchemeVoid();

        private SchemeVoid() { }

        public static SchemeVoid instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SchemeVoid();
                }
                return _instance;
            }
        }

        public override string ToString()
        {
            return "";
        }


    }
}
