using System;
using System.Collections.Generic;

namespace FactoryMethodPattern
{
    using Framework;
    using IDCard;
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new IDCardFactory();
            Product card1 = factory.Create("nyanchu");
            Product card2 = factory.Create("toshi0607");
            card1.Use();
            card2.Use();

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
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
        public string Owner { get; private set; }
        public IDCard(string owner)
        {
            Console.WriteLine($"{owner}のカードを作ります。");
            this.Owner = owner;
        }

        public override void Use()
        {
            Console.WriteLine($"{Owner}のカードを使います。");
        }
    }

    public class IDCardFactory : Factory
    {
        private List<string> Owners { get; set; } = new List<string>();
        protected override Product CreateProduct(string owner)
        {
            return new IDCard(owner);
        }

        protected override void RegisterProduct(Product product)
        {
            Owners.Add(((IDCard)product).Owner);
        }
    }
}