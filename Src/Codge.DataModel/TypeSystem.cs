namespace Codge.DataModel
{
    public class TypeSystem : Namespace
    {
        public TypeSystem()
        {
            TypeSystem = this;

            _types.Add(new BuiltInType(2000, "string", this));
            _types.Add(new BuiltInType(2001, "int", this));
            _types.Add(new BuiltInType(2002, "bool", this));
            _types.Add(new BuiltInType(2003, "double", this));
            _types.Add(new BuiltInType(2004, "decimal", this));
            _types.Add(new BuiltInType(2005, "long", this));
        }
    }
}
