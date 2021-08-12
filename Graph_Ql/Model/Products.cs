using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graph_Ql.Model
{
    public class Products
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset IntroducedAt { get; set; }
        public string PhotoFileName { get; set; }

        public double Price { get; set; }
    }
}
