using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot.Types
{
    public class ItemActive
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string effect { get; set; }

        public string creator { get; set; }
        public bool verified { get; set; }
        public int rating { get; set; }
        public DateTime date { get; set; }
    }
}
