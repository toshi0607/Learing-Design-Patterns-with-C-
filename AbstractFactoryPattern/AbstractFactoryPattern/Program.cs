﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

namespace AbstractFactoryPattern
{
    using Factory;
    class Program
    {
        // Client
        // AbstractFactoryとAbstractProductのインターフェース（API）だけを使って仕事を行う
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Usage: C# Main class.name.of.ConcreteFactory");
                Console.WriteLine("Example 1: C# Main ListFactory.ListFactory");
                Console.WriteLine("Example 2: C# Main TableFactory.TableFactory");
                Environment.Exit(0);
            }
            Factory factory = Factory.GetFactory(args[0]);

            Link asahi = factory.CreateLink("朝日新聞", "http://www.asashi.com/");
            Link yomiuri = factory.CreateLink("読売新聞", "http://www.yomiuri.co.jp/");

            Link usYahoo = factory.CreateLink("Yahoo!", "http://www.yahoo.com/");
            Link jpYahoo = factory.CreateLink("Yahoo!Japan", "http://www.yahoo.co.jp/");
            Link excite = factory.CreateLink("Excite", "http://www.excite.co.jp/");
            Link google = factory.CreateLink("Google", "http://www.google.com/");

            Tray traynews = factory.CreateTray("新聞");
            traynews.Add(asahi);
            traynews.Add(yomiuri);

            Tray trayyahoo = factory.CreateTray("Yahoo!");
            trayyahoo.Add(usYahoo);
            trayyahoo.Add(jpYahoo);

            Tray traysearch = factory.CreateTray("サーチエンジン");
            traysearch.Add(trayyahoo);
            traysearch.Add(excite);
            traysearch.Add(google);

            Page page = factory.CreatePage("LinkPage", "結城 浩");
            page.Add(traynews);
            page.Add(traysearch);
            page.Output();

            // 実行が一瞬で終わって確認できないので、キーの入力を待ちます
            Console.ReadLine();
        }
    }
}

namespace Factory
{
    // AbstractProduct
    // ・AbstractFactoryによって作り出される抽象的な部品や製品のインターフェース（API）を定める
    public abstract class Item
    {
        protected string caption;
        public Item(string caption)
        {
            this.caption = caption;
        }
        public abstract string MakeHTML();
    }

    // AbstractProduct
    public abstract class Link : Item
    {
        protected string url;
        public Link(string caption, string url) : base(caption)
        {
            this.url = url;
        }
    }

    // AbstractProduct
    public abstract class Tray : Item
    {
        protected List<Item> tray = new List<Item>();
        public Tray(string caption) : base(caption) { }

        public void Add(Item item)
        {
            tray.Add(item);
        }
    }

    // AbstractProduct
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
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    writer.Write(this.MakeHTML());
                }
                Console.WriteLine($"{filename}を作成しました。");
            }
            catch(IOException e)
            {
                Console.Error.WriteLine(e);
            }
        }
        public abstract string MakeHTML();
    }

    // AbstractFactory
    // ・AbstractProductのインスタンスを作り出すためのインターフェース（API）を定める
    public abstract class Factory
    {
        public static Factory GetFactory(string classname)
        {
            Factory factory = null;
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                factory = (Factory)assembly.CreateInstance(
                  classname,
                  false,
                  BindingFlags.CreateInstance,
                  null,
                  null,
                  null,
                  null
                );
            }
            catch(TypeLoadException)
            {
                Console.Error.WriteLine($"クラス{classname}が見つかりません。");
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }
            return factory;
        }
        public abstract Link CreateLink(string caption, string url);
        public abstract Tray CreateTray(string caption);
        public abstract Page CreatePage(string title, string author);
    }
}

namespace ListFactory
{
    using Factory;
    
    // ConcreteFactory
    public class ListFactory : Factory
    {
        public override Link CreateLink(string caption, string url)
        {
            return new ListLink(caption, url);
        }

        public override Tray CreateTray(string caption)
        {
            return new ListTray(caption);
        }

        public override Page CreatePage(string title, string author)
        {
            return new ListPage(title, author);
        }

        
    }

    // ConcreteProduct
    public class ListLink : Link
    {
        public ListLink(string caption, string url) : base(caption, url) { }

        public override string MakeHTML()
        {
            return $"  <li><a href=\"{url}\">{caption}</a></li>\n";
        }
    }


    // ConcreteProduct
    public class ListTray : Tray
    {
        public ListTray(string caption) : base(caption) { }

        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>\n");
            sb.Append($"{caption}\n");
            sb.Append("<ul>\n");
            IEnumerator<Item> e = tray.GetEnumerator();
            while (e.MoveNext())
            {
                sb.Append(e.Current.MakeHTML());
            }
            sb.Append("</ul>\n");
            sb.Append("</li>\n");
            return sb.ToString();
        }
    }

    // ConcreteProduct
    public class ListPage : Page
    {
        public ListPage(string title, string author) : base(title, author) { }
        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<html><head><title>{title}</title></head>\n");
            sb.Append("<body>\n");
            sb.Append($"<h1>{title}</h1>");
            sb.Append("<ul>\n");
            IEnumerator<Item> e = content.GetEnumerator();
            while (e.MoveNext())
            {
                sb.Append(e.Current.MakeHTML());
            }
            sb.Append("</ul>\n");
            sb.Append($"<hr><address>{author}</address>");
            sb.Append("</body></html>\n");
            return sb.ToString();
        }
    }
}

namespace TableFactory
{
    using Factory;

    // Concrete Factory
    public class TableFactory : Factory
    {
        public override Link CreateLink(string caption, string url)
        {
            return new TableLink(caption, url);
        }

        public override Tray CreateTray(string caption)
        {
            return new TableTray(caption);
        }

        public override Page CreatePage(string title, string author)
        {
            return new TablePage(title, author);
        }
    }

    // ConcreteProduct
    public class TableLink : Link
    {
        public TableLink(string caption, string url) : base(caption, url) { }

        public override string MakeHTML()
        {
            return $"<td><a href=\"{url}\">{caption}</a></td>\n";
        }
    }

    // ConcreteProduct
    public class TableTray : Tray
    {
        public TableTray(string caption) : base(caption) { }

        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td>");
            sb.Append("<table width=\"100%\" border=\"1\"><tr>");
            sb.Append($"<td bgcolor=\"#cccccc\" align=\"center\" colspan=\"{tray.Count}\"<b>{caption}</b></td>");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            IEnumerator<Item> e = tray.GetEnumerator();
            while (e.MoveNext())
            {
                sb.Append(e.Current.MakeHTML());
            }
            sb.Append("<tr></table>");
            sb.Append("</tr>");
            return sb.ToString();
        }
    }

    // ConcreteProduct
    public class TablePage : Page
    {
        public TablePage(string title, string author) : base(title, author) { }

        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<html><head><title>{title}</title></head>\n");
            sb.Append("<body>\n");
            sb.Append($"<h1>{title}</h1>\n");
            sb.Append("<table width=\"80%\" border=\"3\">\n");
            IEnumerator<Item> e = content.GetEnumerator();
            while (e.MoveNext())
            {
                sb.Append($"<tr>{e.Current.MakeHTML()}</tr>");
            }
            sb.Append("</table>\n");
            sb.Append($"<hr><address>{author}</address>");
            sb.Append("</body></html>\n");
            return sb.ToString();
        }
    }
}