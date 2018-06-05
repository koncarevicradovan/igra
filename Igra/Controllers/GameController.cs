using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Igra.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        [CheckLoginFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}