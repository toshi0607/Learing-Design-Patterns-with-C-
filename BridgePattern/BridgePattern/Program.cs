using System;
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
        }
    }

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

        public void Display()
        {
            Open();
            Print();
            Close();
        }
    }
    
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
}
