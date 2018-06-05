using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igra.DAL
{
    public class ReadyForGame
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Accepted { get; set; }
    }
}