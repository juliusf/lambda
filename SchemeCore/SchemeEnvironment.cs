using System;
using System.Collections.Generic;

using System.Text;

using System.Diagnostics;

using SchemeCore.objects;
using SchemeCore.helper;

namespace SchemeCore
{
    public interface ISchemeEnvironment
    {
        void set(SchemeSymbol symbol, SchemeType type);
        SchemeType get(SchemeSymbol symbol);
        bool has(SchemeSymbol symbol);
        Dictionary<string, SchemeType> getDict();
        ISchemeEnvironment parent();
        void setParent(ISchemeEnvironment parent);
        ISchemeEnvironment getClonedEnv(ISchemeEnvironment parent);
    }

    internal class SchemeEnvironment : ISchemeEnvironment
    {
        private ISchemeEnvironment _parent;
        public Dictionary<string, SchemeType> _symbolTable = new Dictionary<string, SchemeType>();

        public Dictionary<string, SchemeType> getDict()
        {
            return _symbolTable;
        }

        public SchemeEnvironment(Dictionary<string, SchemeType> dict, ISchemeEnvironment parent)
        {
            _parent = parent;
            _symbolTable = dict;
        }

        public SchemeEnvironment(ISchemeEnvironment parent)
        {
            Debug.Assert(parent != null, "Parent must not be null! use SchemeEnvironmentroot.Singleton!");
            _parent = parent;
        }

        public void set(SchemeSymbol symbol, SchemeType type)
        {
            _symbolTable[symbol.value] = type;
        }

        public SchemeType get(SchemeSymbol symbol)
        {
            if (_symbolTable.ContainsKey(symbol.value))
            {
                return _symbolTable[symbol.value];
            }
            else
            {
                return _parent.get(symbol);
            }
        }

        public bool has(SchemeSymbol symbol)
        {
            if (_symbolTable.ContainsKey(symbol.value))
            {
                return true;
            }
            else
            {
                if (parent() == null)
                {
                    return false;
                }
                else
                {
                    return _parent.has(symbol);
                }
            }
        }

        public bool hasLocal(SchemeSymbol symbol)
        {
            return _symbolTable.ContainsKey(symbol.value);
        }

        public ISchemeEnvironment parent()
        {
            return _parent;
        }


        public void setParent(ISchemeEnvironment parent)
        {
            this._parent = parent;
        }



        public ISchemeEnvironment getClonedEnv(ISchemeEnvironment parent)
        {


            var tmp = new SchemeEnvironment(_symbolTable, parent);
            return (ISchemeEnvironment)tmp;
        }


    }

    class SchemeEnvironmentRoot : ISchemeEnvironment
    {
        private static SchemeEnvironmentRoot _instance = new SchemeEnvironmentRoot();
        public Dictionary<string, SchemeType> _symbolTable = new Dictionary<string, SchemeType>();

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

        public void set(SchemeSymbol symbol, SchemeType type)
        {
            _symbolTable[symbol.value] = type;
        }

        public SchemeType get(SchemeSymbol symbol)
        {
            if (_symbolTable.ContainsKey(symbol.value))
            {
                return _symbolTable[symbol.value];
            }
            else
            {
                //check if it is an integer or float
                int intValue;
                if (int.TryParse(symbol.value, out intValue))
                {

                    return new SchemeInteger(intValue);
                }
                else
                {
                    return null;
                }
            }
        }
        public ISchemeEnvironment parent()
        {
            return null;
        }

        public bool has(SchemeSymbol symbol)
        {
            return _symbolTable.ContainsKey(symbol.value);
        }


        public Dictionary<string, SchemeType> getDict()
        {
            return _symbolTable;
        }


        public void setParent(ISchemeEnvironment parent)
        {
            throw new NotImplementedException();
        }
        public ISchemeEnvironment getClonedEnv(ISchemeEnvironment parent)
        {


            return new SchemeEnvironment(_symbolTable, parent);
        }
    }
}