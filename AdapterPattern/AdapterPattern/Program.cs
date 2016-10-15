using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Print p = new PrintBanner("Hello");
            p.PrintWeak();
            p.PrintStrong();
            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }

    // このクラスは既に提供されているものとします
    public class Banner
    {
        private string str;
        public Banner(string str)
        {
            this.str = str;
        }
        public void ShowWithPattern()
        {
            Console.WriteLine($"({str})");
        }
        public void ShowWithAster()
        {
            Console.WriteLine($"*{str}*");
        }
    }
    public interface Print
    {
        void PrintWeak();
        void PrintStrong();
    }
    public class PrintBanner : Banner, Print
    {
        public PrintBanner(string str) : base(str) { }
        public void PrintWeak()
        {
            this.ShowWithPattern();
        }
        public void PrintStrong()
        {
            this.ShowWithAster();
        }
    }

}
