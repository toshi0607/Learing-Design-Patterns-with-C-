using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

namespace Framework
{
    public interface Product : ICloneable
    {
        void Use(string s);
        Product createClone();
    }
}
