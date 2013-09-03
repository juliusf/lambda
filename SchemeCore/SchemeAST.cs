using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SchemeCore.objects;
using SchemeCore.helper;


namespace SchemeCore
{

    
    public class SchemeAST : ICloneable
    {
        protected SchemeAST _parent;
        protected List<SchemeAST> _children  = new List<SchemeAST>();

        public  SchemeObject currentObject { get; set; }
        public bool hasOwnEnviornment { get; set; }
        public List<SchemeAST> children { get { return _children; } set { _children = value; } }
        public SchemeAST parent { get { return _parent; } set { updateParent(value); } }
        public SchemeAST()
        {
            _parent = null;
            this.currentObject = SchemeVoid.instance;
        }

        public SchemeAST( SchemeAST parent, SchemeObject currentObject )
        {
           // Debug.Assert( parent != null && currentObject != null );
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

            return hasChildren ? returnString.Substring( 1 ) : returnString;
        }

        public object Clone()
        {
           
            SchemeAST ast = new SchemeAST( _parent, currentObject );
            ast.hasOwnEnviornment = this.hasOwnEnviornment;

            foreach( SchemeAST child in children )
            {
                ast.children.Add( (SchemeAST) child.Clone() );
            }
            
            return ast;
        
        }

         public override bool Equals(object obj)
         {
           if (obj.GetType() != typeof(SchemeAST))
           {
            return false;
           }
           else
           {
               var other = (SchemeAST) obj;

               if( !other.currentObject.Equals( this.currentObject ) )
               {
                   return false;
               }
            
               if( this.children.Count != other.children.Count )
               {
                   return false;
               } 

               
               for( int i = 0; i < this.children.Count; i++ )
               {
                   if( !(this.children[i]).Equals( other.children[i] ) )
                   {
                       return false;
                   }
               }

               return true;
           }

           
         }

         public void treeSanityCheck()
         {
             bool found = false;
             if (this.parent != null)
             {
                 foreach (SchemeAST child in this.parent.children)
                 {
                     if (child.Equals(this))
                     {
                         found = true;
                     }
                 }

                 if (found == false)
                 {
                     Logger.write("\n");
                     Logger.writeLine(String.Format("TREE CORRUPTION! child: {0} is an orphan!", this.ToString()));
                     if( System.Diagnostics.Debugger.IsAttached )
                     {
                          System.Diagnostics.Debugger.Break();
                     }
                 }
             }
             foreach (SchemeAST child in children)
             {
                 if (! child.parent.Equals(this))
                 {
                     Logger.write("\n");
                     Logger.writeLine(String.Format("TREE CORRUPTION! child: {0} has the wrong parent", child.ToString() ));
                     Logger.writeLine(String.Format("It should be : {0}", this.ToString()));
                     Logger.writeLine(String.Format("Instead it is: {0}", child.parent.ToString() ));
                     Logger.write("\n");
                      if (System.Diagnostics.Debugger.IsAttached)
                         System.Diagnostics.Debugger.Break();
                 }
             }
         }
        public void createTreeOutput()
         {
             SchemeAST curr = this;

             Logger.writeLine("Current AST:");

             curr.createTreeOutputHelper("", true, this);
             Logger.writeLine("----------------------------");

             Logger.writeLine("Timestamp: " + DateTime.Now);
             Logger.writeLine("Whole AST tree:");
             while (curr.currentObject != SchemeVoid.instance)
             {
                 curr = curr.parent;
             }

              curr.createTreeOutputHelper("", true, this);

              Logger.writeLine("============================");


         }

         private void createTreeOutputHelper(string indent, bool last, SchemeAST caller)
         {
             Logger.write(indent);
             if (last)
             {
                 Logger.write("\\-");
                 indent += "  ";
             }
             else
             {
                 Logger.write("|-");
                 indent += "| ";
             }
             if (currentObject.ToString() == "")
             {
                 Logger.write("<void>");
             }
             else
             {
                 Logger.write(currentObject.ToString());
             }
             if( System.Diagnostics.Debugger.IsAttached )
             {
                 treeSanityCheck();
             }
             if (caller == this)
             {
                 Logger.writeLine("  <---");
             }
             else
             {
                 Logger.write("\n");
             }
             for (int i = 0; i < children.Count; i++)
                 children[i].createTreeOutputHelper(indent, i == children.Count - 1, caller);
         }

         public void updateParent(SchemeAST newParent)
         {
             _parent = newParent;
             foreach (SchemeAST child in _children)
             {
                 child.updateParent(this);
             }
         }
        
   
    }

    
}
