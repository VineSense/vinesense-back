using Nickel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nickel.Controllers
{
    public class VinesenseController : Controller
    {
        IServerInformationService ServerInformationService { get; set; }

        public VinesenseController(IServerInformationService serverInformationService)
        {
            ServerInformationService = serverInformationService;
        }

        // GET: Vinesense
        public ActionResult Index()
        {
            ViewBag.Host = ServerInformationService.GetHost();
            ViewBag.MinDate = ServerInformationService.GetMinDate();
            ViewBag.CurrentYear = ServerInformationService.GetCurrentYear();
            ViewBag.SiteNumber = ServerInformationService.GetSiteNumber();
            ViewBag.DepthNumber = ServerInformationService.GetDepthNumber();

            return View();
        }
    }
}