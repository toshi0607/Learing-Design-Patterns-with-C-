using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    using Framework;
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class MessageBox : Product
    {
        private char decochar;
        public MessageBox(char decochar)
        {
            this.decochar = decochar;
        }
        public void Use(string s)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int length  = sjisEnc.GetByteCount(s);
            for (int i = 0; i < length + 4; i++)
            {
                Console.Write(decochar);
            }
            Console.WriteLine("");
            Console.WriteLine($"{decochar} s {decochar}");
            for (int i = 0; i < length + 4; i++)
            {
                Console.Write(decochar);
            }
            Console.WriteLine("");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Product CreateClone()
        {
            return (Product)this.Clone();
        }
    }
}

namespace Framework
{
    public interface Product : ICloneable
    {
        void Use(string s);
        Product CreateClone();
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
            return p.CreateClone();
        }
    }
}
