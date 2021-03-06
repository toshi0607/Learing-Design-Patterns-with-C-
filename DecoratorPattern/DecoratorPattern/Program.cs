﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Display b1 = new StringDisplay("Hello, world.");
            Display b2 = new SideBorder(b1, '#');
            Display b3 = new FullBorder(b2);
            b1.Show();
            b2.Show();
            b3.Show();
            Display b4 =
                new SideBorder(
                    new FullBorder(
                        new SideBorder(
                            new FullBorder(
                                new StringDisplay("こんにちは。")
                                ),
                                '*')
                            ),
                            '/'
                        );
            b4.Show();

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }

    // Component
    // ・機能を追加するときの核
    public abstract class Display
    {
        public abstract int Columns { get; }
        public abstract int Rows { get; }
        public abstract string GetRowText(int row);
        public void Show()
        {
            for(int i = 0; i < Rows; i++)
            {
                Console.WriteLine(GetRowText(i));
            }
        }
    }

    // ConcreteComponent
    // ・Componentを実装
    public class StringDisplay : Display
    {
        private string str;
        public StringDisplay(string str)
        {
            this.str = str;
        }
        public override int Columns
        {
            get
            {
                Encoding sjisEnc = Encoding.GetEncoding("shift_jis");
                return sjisEnc.GetByteCount(str);
            }
        }

        public override int Rows
        {
            get { return 1; }
        }

        public override string GetRowText(int row)
        {
            if (row == 0)
            {
                return str;
            }
            else
            {
                return null;
            }
        }
    }

    // Decorator
    // ・Componentと同じインターフェース（API）を持つ
    // ・飾る対象となるComponentを持つ
    public abstract class Border : Display
    {
        protected Display display;
        protected Border(Display display)
        {
            this.display = display;
        }
    }

    // ConcreteDecorator
    // ・Decoratorを実装
    public class SideBorder : Border
    {
        private char borderChar;
        public SideBorder(Display display, char ch) : base(display)
        {
            this.borderChar = ch;

        }
        public override int Columns
        {
            get
            {
                return 1 + display.Columns + 1;
            }
        }

        public override int Rows
        {
            get
            {
                return display.Rows;
            }
        }

        public override string GetRowText(int row)
        {
            return borderChar + display.GetRowText(row) + borderChar;
        }
    }

    // ConcreteDecorator
    // ・Decoratorを実装
    public class FullBorder : Border
    {
        public FullBorder(Display display) : base(display) { }
        public override int Columns
        {
            get
            {
                return 1 + display.Columns + 1;
            }
        }

        public override int Rows
        {
            get
            {
                return 1 + display.Rows + 1;
            }
        }

        public override string GetRowText(int row)
        {
            if (row == 0)
            {
                return "+" + MakeLine('-', display.Columns) + "+";
            }
            else if (row == display.Rows + 1)
            {
                return "+" + MakeLine('-', display.Columns) + "+";
            }
            else
            {
                return "|" + display.GetRowText(row - 1) + "|";
            }
        }

        private string MakeLine(char ch, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(ch);
            }
            return sb.ToString();
        }
    }
}
