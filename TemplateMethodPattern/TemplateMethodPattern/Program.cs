﻿using System;
using System.Text;

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
            // => << HHHHH >>
            d2.Display();
            // =>
            // +------------+
            // |Hello, Wirld|
            // |Hello, Wirld|
            // |Hello, Wirld|
            // |Hello, Wirld|
            // |Hello, Wirld|
            // +------------+
            d3.Display();
            // =>
            // +------------+
            // |こんにちは！|
            // |こんにちは！|
            // |こんにちは！|
            // |こんにちは！|
            // |こんにちは！|
            // +------------+

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }

    public abstract class AbstractDisplay
    {
        public abstract void Open();
        public abstract void Print();
        public abstract void Close();
        
        // テンプレートメソッド
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

    // テンプレートメソッドの挙動はサブクラスでの実装に依る
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
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            this.Width = sjisEnc.GetByteCount(str);
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