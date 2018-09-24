using Aspose.Cells;
using Igra.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Igra.Controllers
{
    public class GameController : Controller
    {
        private IgraContext db = new IgraContext();

        // GET: Game
        [CheckLoginFilter]
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Download(string username)
        {
            List<Game> games = db.Games.Where(x => x.Player1 == username || x.Player2 == username).ToList();
            Game game = games.OrderBy(x => x.Id).Last();
            List<FifthGame> fifthGames = db.FifthGames.Where(x => x.Player1 == username || x.Player2 == username).ToList();
            FifthGame fifthGame = fifthGames.OrderBy(x => x.Id).Last();
            GamingUser user = db.Users.First(x => x.Username == username);
            GamingUser firstPlayer = db.Users.First(x => x.Username == game.Player1);
            GamingUser secondPlayer = db.Users.First(x => x.Username == game.Player2);

            using (MemoryStream ms = new MemoryStream())
            {
                string pathFile = HttpContext.Server.MapPath(@"~/Content/ExcelTemplate.xlsx");
                Workbook workbook = new Workbook(pathFile);
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.AutoFitColumns();
                worksheet.Name = "Statistika";

                FillTableForGame(game, firstPlayer, secondPlayer, worksheet);
                FillTableForFifthGame(fifthGame, user, worksheet);
                FillSurvey(user, worksheet);
                FillTasks(user, worksheet);
                RemoveSuperfluousErrorWarnings(worksheet);

                string dateTime = "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                string fileName = "Statistika_" + dateTime + ".xlsx";
                workbook.Save(ms, SaveFormat.Xlsx);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
        }

        private void FillTasks(GamingUser user, Worksheet worksheet)
        {
            MarkAnswersFromTasks(user.Question11, "22", worksheet);
            MarkAnswersFromTasks(user.Question12, "24", worksheet);
            MarkAnswersFromTasks(user.Question13, "26", worksheet);
            MarkAnswersFromTasks(user.Question14, "28", worksheet);
            MarkAnswersFromTasks(user.Question15, "30", worksheet);
            MarkAnswersFromTasks(user.Question16, "32", worksheet);
            MarkAnswersFromTasks(user.Question17, "34", worksheet);
            MarkAnswersFromTasks(user.Question18, "36", worksheet);
            MarkAnswersFromTasks(user.Question19, "38", worksheet);
        }

        private static void FillTableForGame(Game game, GamingUser firstPlayer, GamingUser secondPlayer, Worksheet worksheet)
        {
            worksheet.Cells["A2"].Value = firstPlayer.FirstName + " " + firstPlayer.LastName;
            worksheet.Cells["A3"].Value = secondPlayer.FirstName + " " + secondPlayer.LastName;
            worksheet.Cells["B2"].Value = game.Player1Game1Points;
            worksheet.Cells["B3"].Value = game.Player2Game1Points;
            worksheet.Cells["C2"].Value = game.Player1Game2Points;
            worksheet.Cells["C3"].Value = game.Player2Game2Points;
            worksheet.Cells["D2"].Value = game.Player1Game3Points;
            worksheet.Cells["D3"].Value = game.Player2Game3Points;
            worksheet.Cells["E2"].Value = game.Player1Game4Points;
            worksheet.Cells["E3"].Value = game.Player2Game4Points;
            worksheet.Cells["F2"].Value = game.Player1Game1Points + game.Player1Game2Points + game.Player1Game3Points + game.Player1Game4Points;
            worksheet.Cells["F3"].Value = game.Player2Game1Points + game.Player2Game2Points + game.Player2Game3Points + game.Player2Game4Points;
        }

        private static void FillTableForFifthGame(FifthGame fifthGame, GamingUser user, Worksheet worksheet)
        {
            worksheet.Cells["A6"].Value = user.FirstName + " " + user.LastName;

            int gameFivePlayer11Points = (int)fifthGame.Player1Game1Points;
            int gameFivePlayer12Points = (int)fifthGame.Player1Game2Points;
            int gameFivePlayer13Points = (int)fifthGame.Player1Game3Points;
            int gameFivePlayer14Points = (int)fifthGame.Player1Game4Points;
            int gameFivePlayer15Points = (int)fifthGame.Player1Game5Points;

            int gameFivePlayer21Points = (int)fifthGame.Player2Game1Points;
            int gameFivePlayer22Points = (int)fifthGame.Player2Game2Points;
            int gameFivePlayer23Points = (int)fifthGame.Player2Game3Points;
            int gameFivePlayer24Points = (int)fifthGame.Player2Game4Points;
            int gameFivePlayer25Points = (int)fifthGame.Player2Game5Points;


            if (fifthGame.Player2 == user.Username)
            {
                gameFivePlayer11Points = (int)fifthGame.Player2Game1Points;
                gameFivePlayer12Points = (int)fifthGame.Player2Game2Points;
                gameFivePlayer13Points = (int)fifthGame.Player2Game3Points;
                gameFivePlayer14Points = (int)fifthGame.Player2Game4Points;
                gameFivePlayer15Points = (int)fifthGame.Player2Game5Points;

                gameFivePlayer21Points = (int)fifthGame.Player1Game1Points;
                gameFivePlayer22Points = (int)fifthGame.Player1Game2Points;
                gameFivePlayer23Points = (int)fifthGame.Player1Game3Points;
                gameFivePlayer24Points = (int)fifthGame.Player1Game4Points;
                gameFivePlayer25Points = (int)fifthGame.Player1Game5Points;
            }

            worksheet.Cells["B6"].Value = gameFivePlayer11Points;
            worksheet.Cells["C6"].Value = gameFivePlayer12Points;
            worksheet.Cells["D6"].Value = gameFivePlayer13Points;
            worksheet.Cells["E6"].Value = gameFivePlayer14Points;
            worksheet.Cells["F6"].Value = gameFivePlayer15Points;

            worksheet.Cells["B7"].Value = gameFivePlayer21Points;
            worksheet.Cells["C7"].Value = gameFivePlayer22Points;
            worksheet.Cells["D7"].Value = gameFivePlayer23Points;
            worksheet.Cells["E7"].Value = gameFivePlayer24Points;
            worksheet.Cells["F7"].Value = gameFivePlayer25Points;

            worksheet.Cells["G6"].Value = gameFivePlayer11Points + gameFivePlayer12Points + gameFivePlayer13Points + gameFivePlayer14Points + gameFivePlayer15Points;
            worksheet.Cells["G7"].Value = gameFivePlayer21Points + gameFivePlayer22Points + gameFivePlayer23Points + gameFivePlayer24Points + gameFivePlayer25Points;
        }

        private static void FillSurvey(GamingUser user, Worksheet worksheet)
        {
            worksheet.Cells["F10"].Value = user.Question1;
            worksheet.Cells["F11"].Value = user.Question2;
            worksheet.Cells["F12"].Value = user.Question3;
            worksheet.Cells["F13"].Value = user.Question4;
            worksheet.Cells["F14"].Value = user.Question5;
            worksheet.Cells["F15"].Value = user.Question6;
            worksheet.Cells["F16"].Value = user.Question7;
            worksheet.Cells["F17"].Value = user.Question8;
            worksheet.Cells["F18"].Value = user.Question9;
            worksheet.Cells["F19"].Value = user.Question10;
        }

        private string GetLetterFromNumber(string question, string rowNumber)
        {
            if (question == "1")
            {
                return "A" + rowNumber;
            }
            else if (question == "2")
            {
                return "C" + rowNumber;
            }
            return "E" + rowNumber;
        }

        private void MarkAnswersFromTasks(string question, string rowNumber, Worksheet worksheet)
        {
            string cellName = GetLetterFromNumber(question, rowNumber);
            Cell cell = worksheet.Cells[cellName];
            Style style = cell.GetStyle();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = Color.Yellow;
            cell.SetStyle(style);
        }

        private static void RemoveSuperfluousErrorWarnings(Worksheet sheet)
        {

            ErrorCheckOptionCollection opts = sheet.ErrorCheckOptions;
            int index = opts.Add();
            ErrorCheckOption opt = opts[index];
            opt.SetErrorCheck(ErrorCheckType.TextDate, false);
            opt.SetErrorCheck(ErrorCheckType.TextNumber, false);
            opt.AddRange(CellArea.CreateCellArea(0, 0, sheet.Cells.MaxRow + 1, sheet.Cells.MaxColumn + 1));
        }
    }
}