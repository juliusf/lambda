using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SchemeCore.objects;


namespace SchemeCore
{

    
    public class SchemeAST 
    {
        protected SchemeAST _parent;
        protected List<SchemeAST> _children  = new List<SchemeAST>();

        public  SchemeObject currentObject { get; set; }

        public List<SchemeAST> children { get { return _children; } set { _children = value; } }
        public SchemeAST parent { get{return _parent;} }
        public SchemeAST()
        {
            _parent = null;
            this.currentObject = SchemeVoid.instance;
        }

        public SchemeAST( SchemeAST parent, SchemeObject currentObject )
        {
            Debug.Assert( parent != null && currentObject != null );
            _parent = parent;
            this.currentObject = currentObject;
        }

        public override string ToString()
        {
            if( _parent == null )
            {
                return toStringRoot();
            }

            string returnString = "(" ;
            returnString += currentObject.ToString();

            if ( children.Count == 0 )
            {
                return currentObject.ToString();
            }

            foreach( SchemeAST child in children )
            {
                returnString += " ";
                returnString += child.ToString();
            }
            returnString += ")";

            return returnString;
        }

        private string toStringRoot()
        {
            string returnString = currentObject.ToString();
            bool hasChildren = false;

            foreach( SchemeAST child in children )
            {
                hasChildren = true;
                returnString += " ";
                returnString += child.ToString();
            }

            return hasChildren ? returnString.Substring(1) : returnString;
        }

    }
}
