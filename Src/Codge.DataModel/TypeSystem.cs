namespace Codge.DataModel
{
    public class TypeSystem : Namespace
    {
        public TypeSystem()
        {
            TypeSystem = this;

            _types.Add(new BuiltInType("string", this));
            _types.Add(new BuiltInType("int", this));
            _types.Add(new BuiltInType("bool", this));
            _types.Add(new BuiltInType("double", this));
            _types.Add(new BuiltInType("decimal", this));
            _types.Add(new BuiltInType("long", this));
        }
    }
}
