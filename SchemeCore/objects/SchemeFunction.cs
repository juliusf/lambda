using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchemeCore.objects;

namespace SchemeCore.objects
{
    interface ISchemeFunction
    {
        SchemeObject evaluate( List<SchemeObject>  parameters);
    }

    public abstract class SchemeBuiltInFunction  : SchemeType, ISchemeFunction
    {
        public abstract SchemeObject evaluate( List<SchemeObject> parameters );
        public abstract override string ToString();
    }
}
