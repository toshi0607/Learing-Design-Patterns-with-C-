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

    public class Directry : Entry
    {
        private List<Entry> directry = new List<Entry>();
        public Directry(string name)
        {
            Name = name;
        }

        public new int Size
        {
            get
            {
                int size = 0;
                directry.ForEach(d => size += d.Size);
                return size;
            }
        }

        public override Entry Add(Entry entry)
        {
            directry.Add(entry);
            return this;
        }

        protected override void PrintList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this}");
            directry.ForEach(d => PrintList($"{prefix}/{d.Name}"));
        }
    }

}
