using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern
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
    public abstract class Product
    {
        public abstract void Use();
    }

    public abstract class Factory
    {
        public Product Create(string owner)
        {
            Product p = CreateProduct(owner);
            RegisterProduct(p);
            return p;
        }
        protected abstract Product CreateProduct(string owner);
        protected abstract void RegisterProduct(Product product);
    }
}
