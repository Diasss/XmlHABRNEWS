using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    public struct HabrNews
    {
        public string title { get; set; }
        public string link { get; set; }
        public string descript { get; set; }
        public DateTime pubDate { get; set; }

        public override string ToString()
        {
            string str = string.Format("{0} \n--> {1}\n--> {2:dd.MM.yyyy}",
                title, link, pubDate);
            return str;
        }

    }
}
