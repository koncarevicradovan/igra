using Aspose.Cells;
using System; 
using System.Collections.Generic;
using System.IO;
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

        public FileResult Download()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.Cells["A1"].Value = "DA!";
                string dateTime = "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                string fileName = "Statistika_" + dateTime + ".xlsx";
                workbook.Save(ms, SaveFormat.Xlsx);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            
        }
    }
}