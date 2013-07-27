using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.objects
{
    class SchemeVoid : SchemeObject
    {
        private static readonly SchemeVoid instance = new SchemeVoid();

        private SchemeVoid() { }

        public static SchemeVoid Instance { get { return instance; } }
    }
}
