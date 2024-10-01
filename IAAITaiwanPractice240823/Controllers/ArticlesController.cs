using IAAITaiwanPractice240823.Filters;
using IAAITaiwanPractice240823.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Controllers
{
    [RouteFilter]
    public class ArticlesController : Controller
    {
        private DbModel db = new DbModel();

        public ActionResult About()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Organization()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult History()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Member()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Expert()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Jobs()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Licenses()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Refer()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Survey()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ViewBag.Content = article.Content;
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        //// GET: Articles
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Articles/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Articles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Articles/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Articles/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Articles/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Articles/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Articles/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}