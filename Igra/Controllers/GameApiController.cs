using Igra.DAL;
using Igra.Hubs;
using Igra.VM;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Igra.Controllers
{

    [RoutePrefix("api/gameapi")]
    public class GameApiController : ApiController
    {
        private IgraContext db = new IgraContext();

        [HttpPost, Route("checkForOpponent")]
        public IHttpActionResult CheckForOpponent()
        {
            string username = (string)HttpContext.Current.Session["username"];

            var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();

            if (AppCache.AvailablePlayers.Count == 0) {
                AppCache.AvailablePlayers.Add(username);
                return Content(HttpStatusCode.BadRequest, "Nema korisnika na mreži koji su spremni za igru, dodat na cekanje...");
            }  else
            {
                string opponent = AppCache.AvailablePlayers.FirstOrDefault();
                context.Clients.All.tellOpponentThatGameIsCreated(username, opponent);
                return Ok(true);
            }

            context.Clients.All.taskAdded(1, "julija");
            
            return null;
        }
    }
}
