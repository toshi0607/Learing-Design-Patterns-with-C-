using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Singleton
    {
        private static Singleton singleton = new Singleton();
        private Singleton()
        {
            Console.WriteLine("インスタンスを生成しました。");
        }
        public static Singleton GetInstance()
        {
            return singleton;
        }
    }
}
