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

            if (AppCache.AvailablePlayers.Count(x => x != username) == 0)
            {
                AppCache.AvailablePlayers.Add(username);
                return Content(HttpStatusCode.BadRequest, "Nema korisnika na mreži koji su spremni za igru, dodat na cekanje...");
            }
            else
            {
                string opponent = AppCache.AvailablePlayers.FirstOrDefault();
                Game newGame = new Game
                {
                    Player1 = opponent,
                    Player2 = username,
                    Player1Game1Points = 0,
                    Player2Game1Points = 0,
                    Player1Game2Points = 0,
                    Player2Game2Points = 0,
                    Player1Game3Points = 0,
                    Player2Game3Points = 0,
                    Player1Game4Points = 0,
                    Player2Game4Points = 0,
                };
                db.Games.Add(newGame);
                db.SaveChanges();
                context.Clients.All.tellOpponentThatGameIsCreated(opponent, newGame.Id);
                return Ok(newGame.Id);
            }
        }

        [HttpPost, Route("playCard1")]
        public IHttpActionResult PlayCard1([FromBody]PlayCardRequest request)
        {
            string username = (string)HttpContext.Current.Session["username"];
            Game currentGame = db.Games.FirstOrDefault(x => x.Id == request.GameId);
            // ako sam prvi igrac
            if (currentGame.Player1 == username)
            {
                if (request.CardNumber == 1)
                {
                    currentGame.Player1Game1Points += 5;
                }
                else
                {
                    currentGame.Player2Game1Points += 10;
                }
                currentGame.Player1Game1Played = true;
                db.SaveChanges();
                if (currentGame.Player2Game1Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed1(currentGame.Player2, currentGame.Player1, currentGame.Player1Game1Points, currentGame.Player2Game1Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentName = currentGame.Player1,
                        OpponentPoints = currentGame.Player1Game1Points,
                        MyPoints = currentGame.Player2Game1Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }

                // ispod je slozenija verzija igre, koja nije ispravna ali bleji za svaki slucaj
                //if (currentGame.Player2Game1Points == null)
                //{
                //    // upisujemo u bazu odigranu kartu
                //    currentGame.Player1Game1Points = request.CardNumber;
                //    db.SaveChanges();
                //    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");

                //}
                //else
                //{
                //    if (currentGame.Player2Game1Points > request.CardNumber)
                //    {
                //        currentGame.Player2Game1Points = 10;
                //        currentGame.Player1Game1Points = 0;
                //        db.SaveChanges();
                //    } else if (currentGame.Player2Game1Points == request.CardNumber)
                //    {
                //        currentGame.Player2Game1Points = 5;
                //        currentGame.Player1Game1Points = 5;
                //        db.SaveChanges();
                //    } else
                //    {
                //        currentGame.Player2Game1Points = 0;
                //        currentGame.Player1Game1Points = 10;
                //        db.SaveChanges();
                //    }
                //    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                //    context.Clients.All.opponentPlayed1(currentGame.Player2, currentGame.Player2Game1Points);
                //    return Ok(true);
                //}

            }
            else
            // ako sam drugi igrac
            {
                if (request.CardNumber == 1)
                {
                    currentGame.Player2Game1Points += 5;
                }
                else
                {
                    currentGame.Player1Game1Points += 10;
                }
                currentGame.Player2Game1Played = true;
                db.SaveChanges();
                if (currentGame.Player1Game1Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed1(currentGame.Player1, currentGame.Player2, currentGame.Player2Game1Points, currentGame.Player1Game1Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentName = currentGame.Player1,
                        OpponentPoints = currentGame.Player1Game1Points,
                        MyPoints = currentGame.Player2Game1Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }
            }
        }
    }
}
