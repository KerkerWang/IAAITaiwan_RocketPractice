using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Areas.Admin.Controllers
{
    public class MembershipsController : Controller
    {
        // GET: Admin/Membership
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Membership/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Membership/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Membership/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Membership/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Membership/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Membership/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Membership/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}