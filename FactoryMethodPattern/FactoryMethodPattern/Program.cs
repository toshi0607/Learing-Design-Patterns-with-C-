using System;

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

namespace IDCard
{
    using Framework;
    public class IDCard : Product
    {
        private string Owner { get; set; }
        IDCard(string owner)
        {
            Console.WriteLine($"{owner}のカードを作ります。");
            this.Owner = owner;
        }

        public override void Use()
        {
            Console.WriteLine($"{Owner}のカードを作り使います。");
        }
    }
}