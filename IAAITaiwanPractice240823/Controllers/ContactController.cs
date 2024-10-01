using BotDetect.Web.Mvc;
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
    public class ContactController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "ExampleCaptcha", "驗證碼錯誤")]
        public ActionResult Index(Contact contact)
        {
            // Captcha 驗證已經由 CaptchaValidationActionFilter 處理過了
            if (ModelState.IsValid)
            {
                // 在這裡處理提交的表單內容
                db.ContactEntities.Add(contact);
                db.SaveChanges();
                ModelState.Clear();
            }
            MvcCaptcha.ResetCaptcha("ExampleCaptcha");
            return View();
        }
    }
}