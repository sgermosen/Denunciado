using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Denounces.Web.Models;

namespace Denounces.Web.Controllers
{
    using Denounces.Infraestructure;
    using Denounces.Infraestructure.Extensions;
    using Denounces.Web.Helpers;
    using Denounces.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : PsBaseController
    {
        private readonly IConfiguration configuration;

        public HomeController(ApplicationDbContext context, IUserHelper userHelper, ICurrentUserFactory currentUser, IConfiguration configuration) : base(context, userHelper, currentUser)
        {
            this.configuration = configuration;
        }

        //public HomeController(ApplicationDbContext context,
        //    IUserHelper userHelper, IConfiguration configuration) : base(context, userHelper)
        //{
        //    this.configuration = configuration;
        //}

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                //var owner = await GetOwnerAsync();

                //var home = new HomeViewModel
                //{
                //    Persons = await kids.ToListAsync(),
                //   // Owner = owner
                //};

                return View();// home);
            }
            var isRunningOn = this.configuration["IsRunningOn"];

            if (isRunningOn == "Azure")
            {
                return this.RedirectToAction("Login", "Account");
                //return RedirectToAction("IndexWeb");
            }

            return this.RedirectToAction("Login", "Account");
        }

        public IActionResult Lock()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorPage(string message)
        {
            ViewBag.Message = message;

            return View();
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
