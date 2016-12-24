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
        public abstract int Columns { get; set; }
        public abstract int Rows { get; set; }
        public abstract string GetRowText(int row);
        public void Show()
        {
            for(int i = 0; i < Rows; i++)
            {
                Console.WriteLine(GetRowText(i));
            }
        }
    }

}
