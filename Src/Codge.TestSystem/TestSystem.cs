using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Codge.TestSystem
{
    public class TestSystem
    {
        public IDataStorage DataStorage { get; private set; }

        public TestSystem(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        public TestCase GetTestCase(string name)
        {
            return new TestCase(name, this);
        }
    }
}
