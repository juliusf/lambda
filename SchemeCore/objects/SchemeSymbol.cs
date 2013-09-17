using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeCore.objects
{
    class SchemeSymbol : SchemeObject
    {
        public string value { get; set; }

        public SchemeSymbol(string value)
        {
            this.value = value;
        }
        
        public override string ToString()
        {
            return this.value;
        }

        public override bool Equals(object obj)
        {
            if  ( obj.GetType()  != typeof( SchemeSymbol ) )
            {
                return base.Equals( obj );
            }

            var other = ( SchemeSymbol ) obj;

            return value == other.value;
        }
    }
}
