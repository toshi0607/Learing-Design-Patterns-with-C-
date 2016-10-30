﻿using System;

namespace TemplateMethodPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractDisplay d1 = new CharDisplay('H');
            AbstractDisplay d2 = new StringDisplay("Hello, Wirld");
            AbstractDisplay d3 = new StringDisplay("こんにちは！");
            d1.Display();
            d2.Display();
            d3.Display();
            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }

    public abstract class AbstractDisplay
    {
        public abstract void Open();
        public abstract void Print();
        public abstract void Close();
        public void Display()
        {
            this.Open();
            for(int i = 0; i < 5; i++)
            {
                this.Print();
            }
            this.Close();
        }
    }

    public class CharDisplay : AbstractDisplay
    {
        private char Ch { get; set; }
        public CharDisplay(char ch)
        {
            this.Ch = ch;
        }
        public override void Open()
        {
            Console.Write("<<");
        }
        public override void Print()
        {
            Console.Write(Ch);
        }
        public override void Close()
        {
            Console.WriteLine(">>");
        }

    }

    public class StringDisplay : AbstractDisplay
    {
        private string Str { get; set; }
        private int Width { get; set; }
        public StringDisplay(string str)
        {
            this.Str = str;
            this.Width = str.Length;
        }
        public override void Open()
        {
            this.PrintLine();
        }
        public override void Print()
        {
            Console.WriteLine($"|{this.Str}|");
        }
        public override void Close()
        {
            this.PrintLine();
        }
        private void PrintLine()
        {
            Console.Write("+");
            for(int i = 0; i < this.Width; i ++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }

    }
}