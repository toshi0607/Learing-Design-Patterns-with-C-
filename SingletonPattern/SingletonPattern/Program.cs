﻿using System;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start.");
            // => Start.
            Singleton obj1 = Singleton.GetInstance();
            // => インスタンスを生成しました。
            Singleton obj2 = Singleton.GetInstance();
            if (obj1 == obj2)
            {
                Console.WriteLine("obj1とobj2は同じインスタンスです。");
                // => obj1とobj2は同じインスタンスです。
            }
            else
            {
                Console.WriteLine("obj1とobj2は同じ違うインスタンスです。");
            }
            Console.WriteLine("End.");
            // => End.

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
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
