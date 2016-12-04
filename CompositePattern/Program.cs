using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public abstract class Entry
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public virtual Entry Add(Entry entry)
        {
            throw new NotImplementedException();
        }

        public void PrintList()
        {
            PrintList("");
        }

        protected abstract void PrintList(string prefix);

        public override string ToString()
        {
            return $"{Name} ({Size})";
        }
    }

    public class File : Entry
    {
        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        protected override void PrintList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this}");
        }
    }
}
