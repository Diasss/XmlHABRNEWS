using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (HabrNews item in GetHabrNews1())
            //    Console.WriteLine(item);  
            Exmpl03();
        }
        static XmlDocument GetDocument(string resurs)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(resurs);
            return xmlDocument;
        }
        static List<HabrNews> GetHabrNews()
        {
            List<HabrNews> newses = new List<HabrNews>();

            XmlDocument doc = GetDocument("https://habr.com/ru/rss/interesting/");

            foreach (XmlNode root in doc.DocumentElement.ChildNodes)
            {
                foreach (XmlNode channel in root.ChildNodes)
                {
                    if(channel.Name == "item")
                    {
                        HabrNews habrNews = new HabrNews();
                        foreach (XmlNode itemChannel in channel.ChildNodes)
                        {
                            switch(itemChannel.Name)
                            {
                                case "title":
                                    habrNews.title = itemChannel.InnerText;
                                    break;
                                case "description":
                                    habrNews.descript = itemChannel.InnerText;
                                    break;
                                case "link":
                                    habrNews.link = itemChannel.InnerText;
                                    break;
                                case "pubDate":
                                    habrNews.pubDate = Convert.ToDateTime(itemChannel.InnerText);
                                    break;
                            }
                        }
                        newses.Add(habrNews);
                    }
                }
            }
            return newses;
        }
        static List<HabrNews> GetHabrNews1()
        {
            List<HabrNews> newses = new List<HabrNews>();

            XmlDocument doc = GetDocument("https://habr.com/ru/rss/interesting/");

            foreach (XmlNode item in doc.SelectNodes("//rss/channel/item"))
            {
                HabrNews habrNews = new HabrNews();
                habrNews.title = item.SelectSingleNode("title").InnerText;
                habrNews.descript = item.SelectSingleNode("description").InnerText;
                habrNews.link = item.SelectSingleNode("link").InnerText;
                habrNews.pubDate = Convert.ToDateTime(item.SelectSingleNode("pubDate").InnerText);
                newses.Add(habrNews);
            }

            return newses;
        }





        static void Exmpl01()
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration declaration =
                doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            doc.AppendChild(declaration);

            XmlElement root = doc.CreateElement("root");
            XmlElement note = doc.CreateElement("note");

            XmlAttribute date = doc.CreateAttribute("date");
            date.InnerText = DateTime.Now.ToShortDateString();
            root.Attributes.Append(date);

            XmlAttribute isLegal = doc.CreateAttribute("isLegal");
            isLegal.InnerText = "false";
            root.Attributes.Append(isLegal);

            XmlElement message = doc.CreateElement("message");
            message.InnerText = " если жалование < 1000 оплеух";

            note.AppendChild(message);
            root.AppendChild(note);

            doc.AppendChild(root);

            doc.Save("note.xml");
        }

        static void Exmpl02()
        {
            XmlDocument doc = new XmlDocument();

            //1 вариант Парсинг string
            //string result = GetStatusMessage();
            //doc.LoadXml(result);

            //2
            doc.Load("https://news.rambler.ru/rss/world/");

            //3 
            doc.Load("note.xml");
        }

        static string GetStatusMessage()
        {
            return "<result><message>Соообщение обробатывается</message></result>";
        }

        static void Exmpl03()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("note.xml");

            //Добавление
            //XmlElement to = doc.CreateElement("to");
            //to.InnerText = "Yevgen";
            //doc.DocumentElement.InsertAfter(to, null);

            //Удаление
            //XmlNode toR = doc.SelectSingleNode("//root/to");
            //doc.DocumentElement.RemoveChild(toR);

            //Редактирование
            //doc.DocumentElement.SelectSingleNode("to").InnerText = "John";

            doc.Save("note.xml");

        }
    }
}
