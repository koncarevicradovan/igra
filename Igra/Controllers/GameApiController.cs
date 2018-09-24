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
                AppCache.AvailablePlayers.Remove(opponent);
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
                    var myUser = db.Users.FirstOrDefault(x=>x.Username == currentGame.Player1);
                    var myFullName = myUser.FirstName + " " + myUser.LastName;
                    var opponentUser = db.Users.FirstOrDefault(x => x.Username == currentGame.Player2);
                    var opponentFullName = opponentUser.FirstName + " " + opponentUser.LastName;
                    context.Clients.All.opponentPlayed1(currentGame.Player2, myFullName, currentGame.Player1Game1Points, currentGame.Player2Game1Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentName = opponentFullName,
                        OpponentPoints = currentGame.Player2Game1Points,
                        MyPoints = currentGame.Player1Game1Points
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
                    var myUser = db.Users.FirstOrDefault(x => x.Username == currentGame.Player2);
                    var myFullName = myUser.FirstName + " " + myUser.LastName;
                    var opponentUser = db.Users.FirstOrDefault(x => x.Username == currentGame.Player1);
                    var opponentFullName = opponentUser.FirstName + " " + opponentUser.LastName;

                    context.Clients.All.opponentPlayed1(currentGame.Player1, myFullName, currentGame.Player2Game1Points, currentGame.Player1Game1Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentName = opponentFullName,
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

        [HttpPost, Route("playCard2")]
        public IHttpActionResult PlayCard2([FromBody]PlayCardRequest request)
        {
            string username = (string)HttpContext.Current.Session["username"];
            Game currentGame = db.Games.FirstOrDefault(x => x.Id == request.GameId);
            // ako sam prvi igrac
            if (currentGame.Player1 == username)
            {
                if (request.CardNumber == 1)
                {
                    currentGame.Player1Game2Points += 5;
                }
                else
                {
                    currentGame.Player2Game2Points += 10;
                }
                currentGame.Player1Game2Played = true;
                db.SaveChanges();
                if (currentGame.Player2Game2Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed2(currentGame.Player2, currentGame.Player1Game2Points, currentGame.Player2Game2Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = currentGame.Player2Game2Points,
                        MyPoints = currentGame.Player1Game2Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }

            }
            else
            // ako sam drugi igrac
            {
                if (request.CardNumber == 1)
                {
                    currentGame.Player2Game2Points += 5;
                }
                else
                {
                    currentGame.Player1Game2Points += 10;
                }
                currentGame.Player2Game2Played = true;
                db.SaveChanges();
                if (currentGame.Player1Game2Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed2(currentGame.Player1, currentGame.Player2Game2Points, currentGame.Player1Game2Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = currentGame.Player1Game2Points,
                        MyPoints = currentGame.Player2Game2Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }
            }
        }

        [HttpPost, Route("playCard3")]
        public IHttpActionResult PlayCard3([FromBody]PlayCardRequest request)
        {
            string username = (string)HttpContext.Current.Session["username"];
            Game currentGame = db.Games.FirstOrDefault(x => x.Id == request.GameId);
            // ako sam prvi igrac
            if (currentGame.Player1 == username)
            {
                if (request.CardNumber == 2)
                {
                    currentGame.Player2Game3Points += 10;
                }
                currentGame.Player1Game3Played = true;
                db.SaveChanges();
                if (currentGame.Player2Game3Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed3(currentGame.Player2, currentGame.Player1Game3Points, currentGame.Player2Game3Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = currentGame.Player2Game3Points,
                        MyPoints = currentGame.Player1Game3Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }

            }
            else
            // ako sam drugi igrac
            {
                if (request.CardNumber == 2)
                {
                    currentGame.Player1Game3Points += 10;

                }
                currentGame.Player2Game3Played = true;
                db.SaveChanges();
                if (currentGame.Player1Game3Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed3(currentGame.Player1, currentGame.Player2Game3Points, currentGame.Player1Game3Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = currentGame.Player1Game3Points,
                        MyPoints = currentGame.Player2Game3Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }
            }
        }

        [HttpPost, Route("playCard4")]
        public IHttpActionResult PlayCard4([FromBody]PlayCardRequest request)
        {
            string username = (string)HttpContext.Current.Session["username"];
            Game currentGame = db.Games.FirstOrDefault(x => x.Id == request.GameId);
            // ako sam prvi igrac
            if (currentGame.Player1 == username)
            {
                if (request.CardNumber == 1)
                {
                    currentGame.Player1Game4Points += 5;
                }
                else
                {
                    currentGame.Player2Game4Points += 10;
                }
                currentGame.Player1Game4Played = true;
                db.SaveChanges();
                if (currentGame.Player2Game4Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed4(currentGame.Player2, currentGame.Player1Game4Points, currentGame.Player2Game4Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = currentGame.Player2Game4Points,
                        MyPoints = currentGame.Player1Game4Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }

            }
            else
            // ako sam drugi igrac
            {
                if (request.CardNumber == 1)
                {
                    currentGame.Player2Game4Points += 5;
                }
                else
                {
                    currentGame.Player1Game4Points += 10;
                }
                currentGame.Player2Game4Played = true;
                db.SaveChanges();
                if (currentGame.Player1Game4Played)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed4(currentGame.Player1, currentGame.Player2Game4Points, currentGame.Player1Game4Points);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = currentGame.Player1Game4Points,
                        MyPoints = currentGame.Player2Game4Points
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }
            }
        }

        [HttpPost, Route("checkForOpponent2")]
        public IHttpActionResult CheckForOpponent2()
        {
            string username = (string)HttpContext.Current.Session["username"];

            var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();

            if (AppCache.AvailablePlayersFifthGame.Count(x => x != username) == 0)
            {
                AppCache.AvailablePlayersFifthGame.Add(username);
                return Content(HttpStatusCode.BadRequest, "Nema korisnika na mreži koji su spremni za petu igru, dodat na cekanje...");
            }
            else
            {
                string opponent = AppCache.AvailablePlayersFifthGame.FirstOrDefault();
                FifthGame newGame = new FifthGame
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
                    Player1Game5Points = 0,
                    Player2Game5Points = 0
                };
                db.FifthGames.Add(newGame);
                db.SaveChanges();
                context.Clients.All.tellOpponentThatGameIsCreated2(opponent, newGame.Id);
                AppCache.AvailablePlayersFifthGame.Remove(opponent);
                return Ok(newGame.Id);
            }
        }

        [HttpPost, Route("playCard5")]
        public IHttpActionResult PlayCard5([FromBody]PlayCardRequestFifthGame request)
        {
            string username = (string)HttpContext.Current.Session["username"];
            FifthGame currentGame = db.FifthGames.FirstOrDefault(x => x.Id == request.GameId);
            // ako sam prvi igrac
            if (currentGame.Player1 == username)
            {
                if (request.CardNumber == 1)
                {
                    switch (request.NumberOfPlay)
                    {
                        case 1:
                            currentGame.Player1Game1Points += 5;
                            currentGame.Player1Game1Played = true;
                            break;
                        case 2:
                            currentGame.Player1Game2Points += 5;
                            currentGame.Player1Game2Played = true;
                            break;
                        case 3:
                            currentGame.Player1Game3Points += 5;
                            currentGame.Player1Game3Played = true;
                            break;
                        case 4:
                            currentGame.Player1Game4Points += 5;
                            currentGame.Player1Game4Played = true;
                            break;
                        case 5:
                            currentGame.Player1Game5Points += 5;
                            currentGame.Player1Game5Played = true;
                            break;
                    }
                    
                }
                else
                {
                    switch (request.NumberOfPlay)
                    {
                        case 1:
                            currentGame.Player2Game1Points += 10;
                            currentGame.Player1Game1Played = true;

                            break;
                        case 2:
                            currentGame.Player2Game2Points += 10;
                            currentGame.Player1Game2Played = true;
                            break;
                        case 3:
                            currentGame.Player2Game3Points += 10;
                            currentGame.Player1Game3Played = true;
                            break;
                        case 4:
                            currentGame.Player2Game4Points += 10;
                            currentGame.Player1Game4Played = true;
                            break;
                        case 5:
                            currentGame.Player2Game5Points += 10;
                            currentGame.Player1Game5Played = true;
                            break;
                    }
                }
                db.SaveChanges();
                bool isCurrentGamePlayed = (request.NumberOfPlay == 1 && currentGame.Player2Game1Played) ||
                    (request.NumberOfPlay == 2 && currentGame.Player2Game2Played) ||
                    (request.NumberOfPlay == 3 && currentGame.Player2Game3Played) ||
                    (request.NumberOfPlay == 4 && currentGame.Player2Game4Played) ||
                    (request.NumberOfPlay == 5 && currentGame.Player2Game5Played);

                if (isCurrentGamePlayed)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    int player1CurrentGamePoints = 0;
                    int player2CurrentGamePoints = 0;
                    switch (request.NumberOfPlay)
                    {
                        case 1:
                            player1CurrentGamePoints = currentGame.Player1Game1Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game1Points.Value;
                            break;
                        case 2:
                            player1CurrentGamePoints = currentGame.Player1Game2Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game2Points.Value;
                            break;
                        case 3:
                            player1CurrentGamePoints = currentGame.Player1Game3Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game3Points.Value;
                            break;
                        case 4:
                            player1CurrentGamePoints = currentGame.Player1Game4Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game4Points.Value;
                            break;
                        case 5:
                            player1CurrentGamePoints = currentGame.Player1Game5Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game5Points.Value;
                            break;
                    }
                    context.Clients.All.opponentPlayed5(currentGame.Player2, player1CurrentGamePoints, player2CurrentGamePoints);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = player2CurrentGamePoints,
                        MyPoints = player1CurrentGamePoints
                    };
                    return Ok(response);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Cekanje na drugog igraca da odigra...");
                }

            }
            else
            // ako sam drugi igrac
            {
                if (request.CardNumber == 1)
                {
                    switch (request.NumberOfPlay)
                    {
                        case 1:
                            currentGame.Player2Game1Points += 5;
                            currentGame.Player2Game1Played = true;
                            break;
                        case 2:
                            currentGame.Player2Game2Points += 5;
                            currentGame.Player2Game2Played = true;
                            break;
                        case 3:
                            currentGame.Player2Game3Points += 5;
                            currentGame.Player2Game3Played = true;
                            break;
                        case 4:
                            currentGame.Player2Game4Points += 5;
                            currentGame.Player2Game4Played = true;
                            break;
                        case 5:
                            currentGame.Player2Game5Points += 5;
                            currentGame.Player2Game5Played = true;
                            break;
                    }
                }
                else
                {
                    switch (request.NumberOfPlay)
                    {
                        case 1:
                            currentGame.Player1Game1Points += 10;
                            currentGame.Player2Game1Played = true;

                            break;
                        case 2:
                            currentGame.Player1Game2Points += 10;
                            currentGame.Player2Game2Played = true;
                            break;
                        case 3:
                            currentGame.Player1Game3Points += 10;
                            currentGame.Player2Game3Played = true;
                            break;
                        case 4:
                            currentGame.Player1Game4Points += 10;
                            currentGame.Player2Game4Played = true;
                            break;
                        case 5:
                            currentGame.Player1Game5Points += 10;
                            currentGame.Player2Game5Played = true;
                            break;
                    }
                }
                db.SaveChanges();
                bool isCurrentGamePlayed = (request.NumberOfPlay == 1 && currentGame.Player1Game1Played) ||
                    (request.NumberOfPlay == 2 && currentGame.Player1Game2Played) ||
                    (request.NumberOfPlay == 3 && currentGame.Player1Game3Played) ||
                    (request.NumberOfPlay == 4 && currentGame.Player1Game4Played) ||
                    (request.NumberOfPlay == 5 && currentGame.Player1Game5Played);
                if (isCurrentGamePlayed)
                {
                    int player1CurrentGamePoints = 0;
                    int player2CurrentGamePoints = 0;
                    switch (request.NumberOfPlay)
                    {
                        case 1:
                            player1CurrentGamePoints = currentGame.Player1Game1Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game1Points.Value;
                            break;
                        case 2:
                            player1CurrentGamePoints = currentGame.Player1Game2Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game2Points.Value;
                            break;
                        case 3:
                            player1CurrentGamePoints = currentGame.Player1Game3Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game3Points.Value;
                            break;
                        case 4:
                            player1CurrentGamePoints = currentGame.Player1Game4Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game4Points.Value;
                            break;
                        case 5:
                            player1CurrentGamePoints = currentGame.Player1Game5Points.Value;
                            player2CurrentGamePoints = currentGame.Player2Game5Points.Value;
                            break;
                    }

                    var context = GlobalHost.ConnectionManager.GetHubContext<Tasks>();
                    context.Clients.All.opponentPlayed5(currentGame.Player1, player2CurrentGamePoints, player1CurrentGamePoints);
                    PlayCardResponse response = new PlayCardResponse
                    {
                        OpponentPoints = player1CurrentGamePoints,
                        MyPoints = player2CurrentGamePoints
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
