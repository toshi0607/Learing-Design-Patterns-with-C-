﻿using System;

namespace ObjectAdapterPattern
{
    class Program
    {
        // Client
        static void Main(string[] args)
        {
            Print p = new PrintBanner("Hello");
            p.PrintWeak();
            // => (Hello)
            p.PrintStrong();
            // => *Hello*

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }

    // このクラスは既に提供されているものとします
    // Adaptee
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

    // Target
    public abstract class Print
    {
        public abstract void PrintWeak();
        public abstract void PrintStrong();
    }

    // Adapter
    public class PrintBanner : Print
    {
        private Banner banner;
        public PrintBanner(string str)
        {
            this.banner = new Banner(str);
        }
        public override void PrintWeak()
        {
            this.banner.ShowWithPattern();
        }
        public override void PrintStrong()
        {
            this.banner.ShowWithAster();
        }
    }
}
