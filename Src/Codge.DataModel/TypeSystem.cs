using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
{
    public class TypeSystem : Namespace
    {
        private static TypeSystem _instance = new TypeSystem();
        public static TypeSystem Instance { get { return _instance; } }

        public TypeSystem()            
        {
            TypeSystem = this;

            _types.Add(new BuiltInType(2000, "string", this));
            _types.Add(new BuiltInType(2001, "int", this));
            _types.Add(new BuiltInType(2002, "bool", this));
            _types.Add(new BuiltInType(2003, "double", this));
        }
    }
    
}
