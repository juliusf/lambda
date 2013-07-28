using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.objects
{
    class SchemeVoid : SchemeObject
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
            return "<void>";
        }
    }
}
