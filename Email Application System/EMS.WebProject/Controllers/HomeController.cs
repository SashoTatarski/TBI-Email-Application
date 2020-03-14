using EMS.Data;
using EMS.WebProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(Constants.ChangePassRedirect);
            }
            else
            {
                return (User.FindFirst("IsPasswordChanged").Value == "False") ? RedirectToAction("ChangePassword", "User") : RedirectToAction("Index", "Email");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
