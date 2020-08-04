using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Denounces.Back.Models;
using Denounces.Infraestructure;
using Denounces.Web.Helpers;
using Denounces.Infraestructure.Extensions;
using Microsoft.Extensions.Configuration;
using Denounces.Web.Controllers;

namespace Denounces.Back.Controllers
{
    public class HomeController : PsBaseController //Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(ApplicationDbContext context, IUserHelper userHelper, ICurrentUserFactory currentUser, IConfiguration configuration) : base(context, userHelper, currentUser)
        {
            this.configuration = configuration;
        }
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            { 
                return View();
            }
            var isRunningOn = this.configuration["IsRunningOn"];

            if (isRunningOn == "Azure")
            {
                return this.RedirectToAction("Login", "Account");
                //return RedirectToAction("IndexWeb");
            }

            return this.RedirectToAction("Login", "Account");
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
    }
}
