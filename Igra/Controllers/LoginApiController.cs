using Igra.DAL;
using Igra.VM;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Igra.Controllers
{

    [RoutePrefix("api/loginapi")]
    public class LoginApiController : ApiController
    {
        private IgraContext db = new IgraContext();

        [HttpPost, Route("login")]
        //[Route("login")]
        public IHttpActionResult Login([FromBody]LoginRequest loginRequest)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == loginRequest.Username && x.Password == loginRequest.Password);
            if (user != null)
            {
                HttpContext.Current.Session["username"] = user.Username;
                return Ok(true);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Šifra i/ili korisničko ime nisu ispravni.");
            }
        }

        [HttpPost, Route("register")]
        //[Route("register")]
        public IHttpActionResult Register([FromBody]RegisterRequest registerRequest)
        {
            try
            {
                GamingUser user = new GamingUser
                {
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    Password = registerRequest.Password,
                    Username = registerRequest.Username,
                    IsFemale = registerRequest.IsFemale
                };
                db.Users.Add(user);
                db.SaveChanges();
                HttpContext.Current.Session["username"] = user.Username;
                return Ok(true);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, "Neuspešna registracija.");
            }
        }
    }
}
