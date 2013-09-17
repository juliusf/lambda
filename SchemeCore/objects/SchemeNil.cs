using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
