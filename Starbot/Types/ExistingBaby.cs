using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot.Types
{
    public class ExistingBaby
    {
        public string name { get; set; }
        public int cost { get; set; }
        public int hp { get; set; }
        public string type { get; set; }
        public int damage { get; set; }
        public float firerate { get; set; }
        public float recharge { get; set; }
        public string abilities { get; set; }

        public string unlocking { get; set; }
        public string creator { get; set; }
        public ulong id { get; set; }
    }
}
