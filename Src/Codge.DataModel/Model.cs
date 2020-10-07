namespace Codge.DataModel
{

    //class represents a subset of types grouped together
    public class Model
    {
        public Namespace Namespace { get; private set; }

        public Model(Namespace ns)
        {
            Namespace = ns;
        }
    }
}
