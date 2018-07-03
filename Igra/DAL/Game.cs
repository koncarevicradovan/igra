using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igra.DAL
{
    public class Game
    {
        public int Id { get; set; }
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int Player1Game1Points { get; set; }
        public int Player2Game1Points { get; set; }
        public int Player1Game2Points { get; set; }
        public int Player2Game2Points { get; set; }
        public int Player1Game3Points { get; set; }
        public int Player2Game3Points { get; set; }
        public int Player1Game4Points { get; set; }
        public int Player2Game4Points { get; set; }
        public int PlayerWinner { get; set; }
    }
}