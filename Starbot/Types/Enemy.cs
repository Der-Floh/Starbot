using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot.Types
{
    public class Enemy
    {
        public string name { get; set; }
        public int hp { get; set; }
        public string type { get; set; }
        public int damage { get; set; }
        public float firerate { get; set; }
        public float walkspeed { get; set; }
        public string abilities { get; set; }
        public string appearance { get; set; }

        public string creator { get; set; }
        public bool verified { get; set; }
        public int rating { get; set; }
        public DateTime date { get; set; }
    }
}
