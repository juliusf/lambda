using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.objects
{
    abstract class SchemeBool : SchemeAtom
    {
    }

    class SchemeTrue :SchemeBool
    {
        private static SchemeTrue _instance = new SchemeTrue();

        private SchemeTrue() { }

        public static SchemeTrue instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SchemeTrue();
                }
                return _instance;
            }
        }
        public override string ToString()
        {
            return "#t"; 
        }
    }

    class SchemeFalse :SchemeBool
    {
        private static SchemeFalse _instance = new SchemeFalse();

        private SchemeFalse() { }

        public static SchemeFalse instance
        {
            get
            {
                if( _instance == null )
                {
                    _instance = new SchemeFalse();
                }
                return _instance;
            }
        }

        public override string ToString()
        {
            return "#f";
        }
    }
}
