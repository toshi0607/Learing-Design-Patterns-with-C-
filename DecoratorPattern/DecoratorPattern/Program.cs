using System;
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
        }
    }

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

    public abstract class Border : Display
    {
        protected Display display;
        protected Border(Display display)
        {
            this.display = display;
        }
    }

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
