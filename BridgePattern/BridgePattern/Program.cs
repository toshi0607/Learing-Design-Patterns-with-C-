﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Display d1 = new Display(new StringDisplayImpl("Hello, Japan."));
            Display d2 = new CountDisplay(new StringDisplayImpl("Hello, World."));
            CountDisplay d3 = new CountDisplay(new StringDisplayImpl("Hello, Universe."));
            d1.Show();
            // =>
            // +-------------+
            // |Hello, Japan.|
            // +-------------+

            d2.Show();
            // =>
            // +-------------+
            // |hello, world.|
            // +-------------+

            d3.Show();
            // =>
            // +----------------+
            // |Hello, Universe.|
            // +----------------+

            d3.MultiDisplay(5);
            // =>
            // +----------------+
            // |Hello, Universe.|
            // |Hello, Universe.|
            // |Hello, Universe.|
            // |Hello, Universe.|
            // |Hello, Universe.|
            // +----------------+

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }

    // Abstraction
    // ・機能のクラス階層の最上位クラス
    // ・Implementorを使用して基本的なメソッドを実装する
    // ・機能クラスの階層と実装クラスの階層を橋渡しするImplementerを保持
    public class Display
    {
        private DisplayImpl impl;
        public Display(DisplayImpl impl)
        {
            this.impl = impl;
        }

        public void Open()
        {
            impl.RawOpen();
        }

        public void Print()
        {
            impl.RawPrint();
        }

        public void Close()
        {
            impl.RawClose();
        }

        public void Show()
        {
            Open();
            Print();
            Close();
        }
    }
    
    // RefinedAbstraction
    // ・Abstractionに対して機能を追加
    public class CountDisplay : Display
    {
        public CountDisplay(DisplayImpl impl) : base(impl) { }

        public void MultiDisplay(int times)
        {
            Open();
            for (int i = 0; i < times; i++)
            {
                Print();
            }
            Close();
        }
    }

    // Implementor
    // ・実装のクラス階層の最上位クラス
    // ・Abstranctionのインターフェース（API）を規定する
    public abstract class DisplayImpl
    {
        public abstract void RawOpen();
        public abstract void RawPrint();
        public abstract void RawClose();
    }

    // ConcreteImplementator
    // ・Implementatorを具体的に実装する
    public class StringDisplayImpl : DisplayImpl
    {
        private string str;
        private int width;
        public StringDisplayImpl(string str)
        {
            this.str = str;
            Encoding sjisEnc = Encoding.GetEncoding("shift_jis");
            this.width = sjisEnc.GetByteCount(str);
        }

        public override void RawOpen()
        {
            PrintLine();
        }

        public override void RawPrint()
        {
            Console.WriteLine($"|{str}|");
        }

        public override void RawClose()
        {
            PrintLine();
        }

        public void PrintLine()
        {
            Console.Write("+");
            for (int i = 0; i <width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }
    }
}
