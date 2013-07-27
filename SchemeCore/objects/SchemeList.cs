using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore.helper;

namespace SchemeCore.objects
{
    class SchemeList : SchemeObject
    {
        
        public SchemeObject car { get; set; }
        public SchemeObject cdr
        {
            get { return this.cdr; }
            set
            {
                if (value == this)
                {
                    throw new SchemeInvalidArgumentException( "I can't be my own cdr!" );
                }
                this.cdr = value;
            }
        }

        public override string ToString()
        {
            if ( cdr.GetType() == typeof( SchemeList ) )
            { 
                return String.Format( "({0}{1})", car.ToString(), cdr.ToString() );
            }
            else if ( car == SchemeVoid.Instance )
            {
                if ( cdr == SchemeNil.Instance )
                {
                    return "" ; 
                }
                else
                {
                    return cdr.ToString();
                }
            }
            else if ( cdr == SchemeNil.Instance )
            {
                return String.Format( "({1})", car.ToString() ); 
            }
            else
            {
                return String.Format( "({1} . {2}", car.ToString(), cdr.ToString() );   
            }
        }

    }
}
