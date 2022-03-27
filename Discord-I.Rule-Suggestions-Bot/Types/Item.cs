using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_I.Rule_Suggestions_Bot.Types
{
    public class Item
    {
        public string name { get; set; }
        public int cost { get; set; }
        public int tier { get; set; }
        public string description { get; set; }
        public string effect { get; set; }

        public string creator { get; set; }
        public bool verified { get; set; }
        public int rating { get; set; }
        public DateTime date { get; set; }
    }
}
