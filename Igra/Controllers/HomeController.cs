using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Igra.DAL;

namespace Igra.Controllers
{
    public class HomeController : Controller
    {
        private IgraContext db = new IgraContext();

        [CheckLoginFilter]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            int a = db.Users.Count();
            ViewBag.TotalStudents = Session["ads"];
            //db.Users.Add(new GamingUser
            //{
            //    Id = 1,
            //    Username = "Julija",
            //    Password = "2204",
            //    FirstName = "Julija",
            //    LastName = "Stefanovic"
            //});
            //db.SaveChanges();
            //a = db.Users.Count();
            return View();
        }
    }
}
