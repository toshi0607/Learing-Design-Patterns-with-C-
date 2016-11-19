using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AbstractFactoryPattern
{
    using Factory;
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Usage: C# Main class.name.of.ConcreteFactory");
                Console.WriteLine("Example 1: C# Main listfactory.ListFactory");
                Console.WriteLine("Example 2: C# Main tablefactory.TableFactory");
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
                Console.Error.WriteLine(e.StackTrace);
            }
        }
        public abstract string MakeHTML();
    }

    public abstract class Factory
    {
        public static Factory GetFactory(string classname)
        {
            Factory factory = null;
            try
            {
                Type type = Type.GetType("classname");
                factory = (Factory)Activator.CreateInstance(type);
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
    public class ListLink : Link
    {
        public ListLink(string caption, string url) : base(caption, url) { }

        public override string MakeHTML()
        {
            return $" <li><a href=\"{url}>\"{caption}</a></li>\n";
        }
    }

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
            while(e.MoveNext())
            {
                sb.Append(e.Current);
            }
            sb.Append("</ul>\n");
            sb.Append("</li>\n");
            return sb.ToString();
        }
    }

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
            while(e.MoveNext())
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