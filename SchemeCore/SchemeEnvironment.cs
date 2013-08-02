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
    interface ISchemeEnvironment
    {
        void set( SchemeSymbol symbol, SchemeType type );
        SchemeType get( SchemeSymbol symbol );
        bool has( SchemeSymbol symbol );
         ISchemeEnvironment parent();
    }

    class SchemeEnvironment :ISchemeEnvironment
    {
        private ISchemeEnvironment _parent;
        private Dictionary<string, SchemeType> _symbolTable = new Dictionary<string,SchemeType>();

        
        public SchemeEnvironment( ISchemeEnvironment parent )
        {
            Debug.Assert( parent != null, "Parent must not be null! use SchemeEnvironmentroot.Singleton!" );
            _parent = parent;
        }

        public void set( SchemeSymbol symbol, SchemeType type )
        {
            _symbolTable[symbol.value] = type;
        }

        public SchemeType get( SchemeSymbol symbol )
        {
            if( _symbolTable.ContainsKey( symbol.value ) )
            {
                return _symbolTable[symbol.value];
            }
            else
            {
                return _parent.get( symbol );
            }
        }

        public bool has( SchemeSymbol symbol )
        {
            return _symbolTable.ContainsKey( symbol.value );
        }

        public ISchemeEnvironment parent()
        {
            return _parent;
        }
    }

    class SchemeEnvironmentRoot :ISchemeEnvironment
    {
        private static SchemeEnvironmentRoot _instance = new SchemeEnvironmentRoot();
        private Dictionary<string, SchemeType> _symbolTable = new Dictionary<string,SchemeType>();
       
        private SchemeEnvironmentRoot() { }

        public static SchemeEnvironmentRoot instance
         {
            get
            {
                if (_instance == null)
                {
                    _instance = new SchemeEnvironmentRoot();
                }
                return _instance;
            }
        }

        public void set( SchemeSymbol symbol, SchemeType type )
        {
            _symbolTable[symbol.value] = type;
        }

        public SchemeType get( SchemeSymbol symbol )
        {
            if( _symbolTable.ContainsKey( symbol.value ) )
            {
                return _symbolTable[symbol.value];
            }
            else
            {
                return null; 
            }
        }
        public ISchemeEnvironment parent()
        {
            return null;
        }
       
        public bool has( SchemeSymbol symbol )
        {
            return _symbolTable.ContainsKey( symbol.value );
        }       
    }
}