﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AbstractFactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

namespace Factory
{
    public abstract class Item
    {
        protected string caption;
        public Item(string caption)
        {
            this.caption = caption;
        }
        public abstract string MakeHTML();
    }

    public abstract class Link : Item
    {
        protected string url;
        public Link(string caption, string url) : base(caption)
        {
            this.url = url;
        }
    }

    public abstract class Tray : Item
    {
        protected List<Item> tray = new List<Item>();
        public Tray(string caption) : base(caption) { }

        public void Add(Item item)
        {
            tray.Add(item);
        }
    }

    public abstract class Page
    {
        protected string title;
        protected string author;
        protected List<Item> content = new List<Item>();
        public Page(string title, string author)
        {
            this.title = title;
            this.author = author;
        }

        public void Add(Item item)
        {
            this.content.Add(item);
        }

        public void Output()
        {
            try
            {
                string filename = title + ".html";
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(this.MakeHTML());
                }
                Console.WriteLine($"{filename}を作成しました。");
            }
            catch(IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public abstract string MakeHTML();
    }
}
