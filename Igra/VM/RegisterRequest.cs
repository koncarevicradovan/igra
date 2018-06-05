using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Igra.VM
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsFemale { get; set; }
    }
}