using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore.helper;

namespace SchemeCore.objects
{
    class SchemeList : SchemeType
    {
        private SchemeObject _car;
        private SchemeObject _cdr;

        public SchemeList(SchemeObject foo, SchemeObject bar)
        {
            this.car = foo;
            this.cdr = bar;
        }

        public SchemeList(SchemeObject car)
        {
            this.car = car;
            this.cdr = SchemeNil.instance;
        }

        public SchemeObject car { get { return _car; } set { _car = value; } }
        public SchemeObject cdr
        {
            get { return _cdr; }
            set
            {
                if (value == this)
                {
                    throw new SchemeInvalidArgumentException( "I can't be my own cdr!" );
                }
                _cdr = value;
            }
        }

        public override string ToString()
        {
            if ( cdr.GetType() == typeof( SchemeList ) )
            { 
                return String.Format( "({0} {1})", car.ToString(), cdr.ToString() );
            }
            else if ( car == SchemeVoid.instance )
            {
                if ( cdr == SchemeNil.instance )
                {
                    return "" ; 
                }
                else
                {
                    return cdr.ToString();
                }
            }
            else if ( cdr == SchemeNil.instance )
            {
                return String.Format( "({0})", car.ToString() ); 
            }
            else
            {
                return String.Format( "({0} . {1})", car.ToString(), cdr.ToString() );   
            }
        }

    }
}
