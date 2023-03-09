using Microsoft.AspNetCore.Mvc;
using UserActionTrackingApp.Models;
using UserActionTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace UserActionTrackingApp.Controllers
{
    public class OtherController : AbstractClassController
    {

        public IActionResult Index ()
        {
            ViewBag.message = TrackingMessage("Other");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
