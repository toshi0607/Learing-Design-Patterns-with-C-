using System;

namespace TemplateMethodPattern
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.WriteLine("<<");
        }
        public override void Print()
        {
            Console.WriteLine(Ch);
        }
        public override void Close()
        {
            Console.WriteLine(">>");
        }

    }

}