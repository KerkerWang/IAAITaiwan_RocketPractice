using IAAITaiwanPractice240823.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Controllers
{
    public class HomeController : Controller
    {
        private DbModel db = new DbModel();

        public ActionResult Index()
        {
            List<News> newsList = db.NewsEntities.Take(4).ToList();
            return View(newsList);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}