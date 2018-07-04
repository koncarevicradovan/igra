using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igra.VM
{
    public class PlayCardResponse
    {
        public string OpponentName { get; set; }
        public int? OpponentPoints { get; set; }
        public int? MyPoints { get; set; }
    }
}