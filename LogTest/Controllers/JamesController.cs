using System.Runtime.CompilerServices;
using System.Web.Mvc;
using MessageCanguru;
using Serilog;
using Serilog.Formatting.Json;

namespace LogTest.Controllers
{
    public class JamesController : Controller
    {

        public ActionResult Index()
        {
            Log.Logger.Information("James controller - Index: Hallo!");

            return View();
        }
        
    }
}