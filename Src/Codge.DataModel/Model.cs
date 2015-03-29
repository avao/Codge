using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
{
    
    //class represents a subset of types groupped together
    public class Model
    {
        public Namespace Namespace { get; private set; }

        public Model(Namespace ns)
        {
            Namespace = ns;
        }
    }

}
