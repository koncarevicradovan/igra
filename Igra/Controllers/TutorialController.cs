using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Igra.DAL;

namespace Igra.Controllers
{
    public class TutorialController : Controller
    {
        private IgraContext db = new IgraContext();

        [CheckLoginFilter]
        public ActionResult Index()
        {
            ViewBag.Title = "Tutorial strana";
            return View();
        }
    }
}
