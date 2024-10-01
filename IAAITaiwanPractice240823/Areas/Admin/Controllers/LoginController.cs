using IAAITaiwanPractice240823.Models;
using IAAITaiwanPractice240823.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace IAAITaiwanPractice240823.Areas.Admin.Controllers
{
    //[Authorize]
    public class LoginController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginVM memberVM)
        {
            if (ModelState.IsValid)
            {
                //1.檢查使用者是否存在
                Member member = ValidateUser(memberVM.Account, memberVM.Password);
                if (member == null)
                {
                    //登入失敗
                    ViewBag.message = "登入失敗";
                    return View(memberVM);
                }
                //登入成功，表單驗證
                string userData = JsonConvert.SerializeObject(member);
                HttpCookie ticket = Utility.SetAuthenTicket(userData, member.Id.ToString());

                //將 Cookie 寫入回應
                Response.Cookies.Add(ticket);

                return RedirectToAction("Index", "Members");
            }
            return View(memberVM);
        }

        /// <summary>
        /// 驗證使用者
        /// </summary>
        /// <param name="account">輸入帳號</param>
        /// <param name="password">輸入密碼</param>
        /// <returns></returns>
        private Member ValidateUser(string account, string password)
        {
            //確認帳號是否存在
            Member member = db.MemberEntities.FirstOrDefault(m => m.Account == account);
            if (member == null)
            {
                return null;
            }
            //確認密碼是否正確
            //從資料庫拿資料
            string dbPassword = member.Password;
            string salt = member.PasswordSalt;
            //產生雜湊密碼
            var hashPassword = Utility.CreatePasswordHash(password, salt);
            if (hashPassword != dbPassword)
            {
                return null;
            }
            return member;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}