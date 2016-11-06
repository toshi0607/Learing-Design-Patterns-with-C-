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

    public class Manager
    {
        private Dictionary<string, Product> showcase = new Dictionary<string, Product>();
        public void Register(string name, Product proto)
        {
            showcase.Add(name, proto);
        }
        public Product Create(string protoname)
        {
            Product p = showcase[protoname];
            return p.createClone();
        }
    }
}
