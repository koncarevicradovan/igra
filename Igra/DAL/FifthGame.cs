using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igra.DAL
{
    public class FifthGame
    {
        public int Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int? Player1Game1Points { get; set; }
        public int? Player2Game1Points { get; set; }
        public int? Player1Game2Points { get; set; }
        public int? Player2Game2Points { get; set; }
        public int? Player1Game3Points { get; set; }
        public int? Player2Game3Points { get; set; }
        public int? Player1Game4Points { get; set; }
        public int? Player2Game4Points { get; set; }
        public int? Player1Game5Points { get; set; }
        public int? Player2Game5Points { get; set; }
        public bool Player1Game1Played { get; set; }
        public bool Player2Game1Played { get; set; }
        public bool Player1Game2Played { get; set; }
        public bool Player2Game2Played { get; set; }
        public bool Player1Game3Played { get; set; }
        public bool Player2Game3Played { get; set; }
        public bool Player1Game4Played { get; set; }
        public bool Player2Game4Played { get; set; }
        public bool Player1Game5Played { get; set; }
        public bool Player2Game5Played { get; set; }
        public int PlayerWinner { get; set; }
    }
}