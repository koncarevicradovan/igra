using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igra.VM
{
    public class AppCache
    {
        public static List<string> AvailablePlayers { get; set; }
        public static List<string> AvailablePlayersFifthGame { get; set; }
        public static List<Pair> Pairs { get; set; }
    }

    public class Pair
    {
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public bool Player1Accepted { get; set; }
        public bool Player2Accepted { get; set; }
    }
}