using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot.Types
{
    public class ExistingItemActive
    {
        public string name { get; set; }
        public string description { get; set; }
        public string effect { get; set; }

        public string unlocking { get; set; }
        public string creator { get; set; }
        public ulong id { get; set; }
    }
}
