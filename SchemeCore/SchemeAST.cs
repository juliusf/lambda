using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SchemeCore.objects;


namespace SchemeCore
{

    
    public class SchemeAST : ICloneable
    {
        protected SchemeAST _parent;
        protected List<SchemeAST> _children  = new List<SchemeAST>();

        public  SchemeObject currentObject { get; set; }

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

             /*  if( this.parent != null && other.parent != null )
               {
                   if( !other.parent.Equals( this.parent ) )
                   {
                       return false;
                   }
               }
               else
               {
                   if( this.parent != other.parent )
                   {
                       return false;
                   }
               } */
            
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
                     Console.Out.Write("\n");
                     Console.Out.WriteLine(String.Format("TREE CORRUPTION! child: {0} is an orphan!", this.ToString()));
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
                     Console.Out.Write("\n");
                     Console.Out.WriteLine(String.Format("TREE CORRUPTION! child: {0} has the wrong parent", child.ToString() ));
                     Console.Out.WriteLine(String.Format("It should be : {0}", this.ToString()));
                     Console.Out.WriteLine(String.Format("Instead it is: {0}", child.parent.ToString() ));
                     Console.Out.Write("\n");
                      if (System.Diagnostics.Debugger.IsAttached)
                         System.Diagnostics.Debugger.Break();
                 }
             }
         }
        public void createTreeOutput()
         {
             SchemeAST curr = this;

             Console.WriteLine("Current AST:");

             curr.createTreeOutputHelper("", true, this);
             Console.WriteLine("----------------------------");

             Console.WriteLine("Timestamp: " + DateTime.Now);
             Console.WriteLine("Whole AST tree:");
             while (curr.currentObject != SchemeVoid.instance)
             {
                 curr = curr.parent;
             }

              curr.createTreeOutputHelper("", true, this);

              Console.WriteLine("============================");


         }

         private void createTreeOutputHelper(string indent, bool last, SchemeAST caller)
         {
             Console.Write(indent);
             if (last)
             {
                 Console.Write("\\-");
                 indent += "  ";
             }
             else
             {
                 Console.Write("|-");
                 indent += "| ";
             }
             if (currentObject.ToString() == "")
             {
                 Console.Write("<void>");
             }
             else
             {
                 Console.Write(currentObject.ToString());
             }
             treeSanityCheck();
             if (caller == this)
             {
                 Console.WriteLine("  <---");
             }
             else
             {
                 Console.Write("\n");
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
