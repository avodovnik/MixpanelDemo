using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo1.Infrastructure;
using Segment.Model;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Segment.Analytics.Client.Track(User.Identity.GetUserEmail(), "Index");
            return View();
        }

        public ActionResult About()
        {
            Segment.Analytics.Client.Track(User.Identity.GetUserEmail(), "About");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            Segment.Analytics.Client.Track(User.Identity.GetUserEmail(), "Contact");

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            Segment.Analytics.Client.Track(User.Identity.GetUserEmail(), "Details", new Properties()
            {
                { "OfferId", id },
                { "XY", "sdasda"}
            });

            return View(id);
        }
    }
}