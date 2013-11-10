using System;
using System.Collections.Generic;

using System.Text;


namespace SchemeCore.objects
{
    class SchemeNil : SchemeAtom
    {
        private static SchemeNil _instance = new SchemeNil();

        private SchemeNil() { }

        public static SchemeNil instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SchemeNil();
                }
                return _instance;
            }
        }


        public override string ToString()
        {
            return "()";
        }

    }

}
