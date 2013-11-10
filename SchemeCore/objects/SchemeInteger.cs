using System;

namespace SchemeCore
{
    public class SchemeInteger : SchemeAtom
    {

        public int value
        {
            get;
            set;
        }

        public SchemeInteger(int value)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(SchemeInteger))
            {
                return base.Equals(obj);
            }

            var other = (SchemeInteger)obj;

            return other.value == value;
        }

        public override string ToString()
        {
            return value.ToString();
        }


    }
}

