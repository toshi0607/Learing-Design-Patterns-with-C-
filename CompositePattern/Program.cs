using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    class Program
    {
        // Client
        // ・Compositeパターンの利用者
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Making root entries...");
                Directry rootdir = new Directry("root");
                Directry bindir = new Directry("bin");
                Directry tmpdir = new Directry("tmp");
                Directry usrdir = new Directry("usr");
                rootdir.Add(bindir);
                rootdir.Add(tmpdir);
                rootdir.Add(usrdir);
                bindir.Add(new File("vi", 10000));
                bindir.Add(new File("latex", 20000));
                rootdir.PrintList();

                Console.WriteLine("");
                Console.WriteLine("Making user entries...");
                Directry nyanchu = new Directry("nyanchu");
                Directry nc = new Directry("nc");
                Directry toshi0607 = new Directry("toshi0607");
                usrdir.Add(nyanchu);
                usrdir.Add(nc);
                usrdir.Add(toshi0607);
                nyanchu.Add(new File("diary.html", 100));
                nyanchu.Add(new File("composite.cs", 200));
                nc.Add(new File("memo.txt", 300));
                toshi0607.Add(new File("game.doc", 400));
                toshi0607.Add(new File("junk.mail", 500));
                rootdir.PrintList();
            }
            catch (FileTreatmentException e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }

    class FileTreatmentException : Exception
    {
        public FileTreatmentException() { }
        public FileTreatmentException(string msg) : base(msg) { }
    }

    // Component
    // ・LeafとCompositeを同一視するためのクラス
    // ・LeafとCompositeの基底クラスとして実現する
    public abstract class Entry
    {
        public string Name { get; set; }
        public virtual int Size { get; set; }
        public virtual Entry Add(Entry entry)
        {
            throw new FileTreatmentException();
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

    // Leaf
    // ・「中身」を表すクラス
    // ・中に他のクラスを入れることはできない
    public class File : Entry
    {
        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        protected override void PrintList(string prefix)
        {
            Console.WriteLine($"{prefix}/{this.ToString()}");
        }
    }

    // Composite
    // ・「容器」を表すクラス
    // ・LeafやCompositeを入れることができる
    public class Directry : Entry
    {
        private List<Entry> directry = new List<Entry>();
        public Directry(string name)
        {
            Name = name;
        }

        public override int Size
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
            Console.WriteLine($"{prefix}/{this.ToString()}");
            directry.ForEach(d => Console.WriteLine($"{prefix}/{d.Name}"));
        }
    }
}
