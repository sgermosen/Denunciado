namespace Denounces.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = "Admin,User")]
    public class OptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}