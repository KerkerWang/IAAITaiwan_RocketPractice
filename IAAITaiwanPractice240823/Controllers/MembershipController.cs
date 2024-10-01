using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using BotDetect.Web.Mvc;
using Dropbox.Api.Users;
using IAAITaiwanPractice240823.Filters;
using IAAITaiwanPractice240823.Models;
using IAAITaiwanPractice240823.Models.ViewModel;
using MvcPaging;

namespace IAAITaiwanPractice240823.Controllers
{
    [RouteFilter]
    public class MembershipController : Controller
    {
        private DbModel db = new DbModel();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "ExampleCaptcha", "驗證碼錯誤")]
        public ActionResult Register(MembershipCreateVM membership, HttpPostedFileBase File)
        {
            // Captcha 驗證已經由 CaptchaValidationActionFilter 處理過了
            if (ModelState.IsValid)
            {
                if (db.MembershipEntities.Any(m => m.Account == membership.Account))
                {
                    //ModelState.Clear();
                    ViewBag.AccountAlert = "<i class=\"fa fa-exclamation-circle\"></i>此帳號已有人使用";
                    return View();
                }
                // 在這裡處理提交的表單內容
                Membership newMembership = new Membership()
                {
                    Account = membership.Account,
                    PasswordSalt = Utility.CreateSalt(5),
                    Name = membership.Name,
                    Gender = membership.Gender.Value,
                    BirthDate = membership.BirthDate,
                    Type = membership.Type.Value,
                    TelPhone = membership.TelPhone,
                    MobilePhone = membership.MobilePhone,
                    Address = membership.Address,
                    Email = membership.Email,
                    IsValid = membership.IsValid,
                    Unit = membership.Unit,
                    JobTitle = membership.JobTitle,
                    Educational = membership.Educational,
                    HistoryUnit1 = membership.HistoryUnit1,
                    HistoryJobTitle1 = membership.HistoryJobTitle1,
                    StartYear1 = membership.StartYear1,
                    StartMonth1 = membership.StartMonth1,
                    EndYear1 = membership.EndYear1,
                    EndMonth1 = membership.EndMonth1,
                    HistoryUnit2 = membership.HistoryUnit2,
                    HistoryJobTitle2 = membership.HistoryJobTitle2,
                    StartYear2 = membership.StartYear2,
                    StartMonth2 = membership.StartMonth2,
                    EndYear2 = membership.EndYear2,
                    EndMonth2 = membership.EndMonth2,
                    HistoryUnit3 = membership.HistoryUnit3,
                    HistoryJobTitle3 = membership.HistoryJobTitle3,
                    StartYear3 = membership.StartYear3,
                    StartMonth3 = membership.StartMonth3,
                    EndYear3 = membership.EndYear3,
                    EndMonth3 = membership.EndMonth3,
                    TotalYear = membership.TotalYear,
                    TotalMonth = membership.TotalMonth,
                };
                newMembership.Password = Utility.CreatePasswordHash(membership.Password, newMembership.PasswordSalt);
                newMembership.LastUpdateTime = newMembership.CreateTime;

                if (membership.IsValid == true && File == null)
                {
                    ViewBag.FileAlert = "<i class=\"fa fa-exclamation-circle\"></i> 請上傳會員證影本";
                    return View();
                }
                //會員證檔案處理及路徑設定
                if (File != null && File.ContentLength > 0)
                {
                    // 取得檔案名稱
                    var fileName = Path.GetFileName(File.FileName);

                    // 指定存放檔案的路徑
                    var path = Path.Combine(Server.MapPath("~/CertificateFiles"), fileName);

                    // 將檔案保存到指定路徑
                    File.SaveAs(path);

                    // 設定封面照片的 URL
                    newMembership.CertificatePath = Url.Content(Path.Combine("~/CertificateFiles", fileName));
                }
                //else
                //{
                //    newMembership.CertificatePath = "";
                //}

                db.MembershipEntities.Add(newMembership);
                db.SaveChanges();
                ModelState.Clear();
            }
            MvcCaptcha.ResetCaptcha("ExampleCaptcha");
            return RedirectToAction("Login", "Membership");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MembershipLoginVM login)
        {
            if (ModelState.IsValid)
            {
                Membership member = db.MembershipEntities.FirstOrDefault(m => m.Account == login.Account);
                if (member == null)
                {
                    ViewBag.AccountAlert = "<i class=\"fa fa-exclamation-circle\"></i>登入失敗";
                    return View();
                }
                string hashPassword = Utility.CreatePasswordHash(login.Password, member.PasswordSalt);
                if (member.Password != hashPassword)
                {
                    ViewBag.AccountAlert = "<i class=\"fa fa-exclamation-circle\"></i>登入失敗";
                    return View();
                }
                Session["membershipId"] = member.Id;
                Session["IsLoggedIn"] = true;
                return RedirectToAction("Board", "Membership");
            }
            return View();
        }

        public ActionResult Logout()
        {
            // 清除 Session 中的所有資料
            Session.Clear();
            Session.Abandon();

            // 重導至登入頁面或首頁
            return RedirectToAction("Login", "Membership");
        }

        [MembershipLoginFilter]
        public ActionResult Board(int? page)
        {
            //設定一頁幾筆
            int DefaultPageSize = 10;

            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.CommentEntities.Count();

            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.CommentEntities.OrderByDescending(p => p.CreateTime).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        [MembershipLoginFilter]
        public ActionResult Edit()
        {
            int membershipId = Convert.ToInt32(Session["membershipId"]);
            Membership membership = db.MembershipEntities.FirstOrDefault(m => m.Id == membershipId);
            if (membership == null)
            {
                return RedirectToAction("Logout");
            }
            MembershipEditVM membershipEditVM = new MembershipEditVM()
            {
                Id = membership.Id,
                Account = membership.Account,
                PasswordSalt = membership.PasswordSalt,
                Password = membership.Password,
                Name = membership.Name,
                Gender = membership.Gender,
                BirthDate = membership.BirthDate,
                Type = membership.Type,
                TelPhone = membership.TelPhone,
                MobilePhone = membership.MobilePhone,
                Address = membership.Address,
                Email = membership.Email,
                IsValid = membership.IsValid,
                CertificatePath = String.IsNullOrEmpty(membership.CertificatePath) ? "" : membership.CertificatePath,
                Unit = membership.Unit,
                JobTitle = membership.JobTitle,
                Educational = membership.Educational,
                HistoryUnit1 = membership.HistoryUnit1,
                HistoryJobTitle1 = membership.HistoryJobTitle1,
                StartYear1 = membership.StartYear1,
                StartMonth1 = membership.StartMonth1,
                EndYear1 = membership.EndYear1,
                EndMonth1 = membership.EndMonth1,
                HistoryUnit2 = String.IsNullOrEmpty(membership.HistoryUnit2) ? membership.HistoryUnit2 : "",
                HistoryJobTitle2 = String.IsNullOrEmpty(membership.HistoryJobTitle2) ? membership.HistoryJobTitle2 : "",
                StartYear2 = String.IsNullOrEmpty(membership.StartYear2) ? membership.StartYear2 : "",
                StartMonth2 = String.IsNullOrEmpty(membership.StartMonth2) ? membership.StartMonth2 : "",
                EndYear2 = String.IsNullOrEmpty(membership.EndYear2) ? membership.EndYear2 : "",
                EndMonth2 = String.IsNullOrEmpty(membership.EndMonth2) ? membership.EndMonth2 : "",
                HistoryUnit3 = String.IsNullOrEmpty(membership.HistoryUnit3) ? membership.HistoryUnit3 : "",
                HistoryJobTitle3 = String.IsNullOrEmpty(membership.HistoryJobTitle3) ? membership.HistoryJobTitle3 : "",
                StartYear3 = String.IsNullOrEmpty(membership.StartYear3) ? membership.StartYear3 : "",
                StartMonth3 = String.IsNullOrEmpty(membership.StartMonth3) ? membership.StartMonth3 : "",
                EndYear3 = String.IsNullOrEmpty(membership.EndYear3) ? membership.EndYear3 : "",
                EndMonth3 = String.IsNullOrEmpty(membership.EndMonth3) ? membership.EndMonth3 : "",
                TotalYear = membership.TotalYear,
                TotalMonth = membership.TotalMonth,
                CreateTime = membership.CreateTime,
            };
            return View(membershipEditVM);
        }

        [HttpPost]
        [MembershipLoginFilter]
        public ActionResult Edit(MembershipEditVM membershipEditVM, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                Membership membership = db.MembershipEntities.FirstOrDefault(m => m.Id == membershipEditVM.Id);
                if (membership == null)
                {
                    return RedirectToAction("Logout");
                }
                membership.Account = membershipEditVM.Account;
                membership.PasswordSalt = membershipEditVM.PasswordSalt;
                membership.Password = String.IsNullOrEmpty(membershipEditVM.NewPassword) ? membershipEditVM.Password : Utility.CreatePasswordHash(membershipEditVM.NewPassword, membershipEditVM.PasswordSalt);
                membership.Name = membershipEditVM.Name;
                membership.Gender = membershipEditVM.Gender.Value;
                membership.BirthDate = membershipEditVM.BirthDate;
                membership.Type = membershipEditVM.Type.Value;
                membership.TelPhone = membershipEditVM.TelPhone;
                membership.MobilePhone = membershipEditVM.MobilePhone;
                membership.Address = membershipEditVM.Address;
                membership.Email = membershipEditVM.Email;
                membership.IsValid = membershipEditVM.IsValid;
                membership.Unit = membershipEditVM.Unit;
                membership.JobTitle = membershipEditVM.JobTitle;
                membership.Educational = membershipEditVM.Educational;
                membership.HistoryUnit1 = membershipEditVM.HistoryUnit1;
                membership.HistoryJobTitle1 = membershipEditVM.HistoryJobTitle1;
                membership.StartYear1 = membershipEditVM.StartYear1;
                membership.StartMonth1 = membershipEditVM.StartMonth1;
                membership.EndYear1 = membershipEditVM.EndYear1;
                membership.EndMonth1 = membershipEditVM.EndMonth1;
                membership.HistoryUnit2 = membershipEditVM.HistoryUnit2;
                membership.HistoryJobTitle2 = membershipEditVM.HistoryJobTitle2;
                membership.StartYear2 = membershipEditVM.StartYear2;
                membership.StartMonth2 = membershipEditVM.StartMonth2;
                membership.EndYear2 = membershipEditVM.EndYear2;
                membership.EndMonth2 = membershipEditVM.EndMonth2;
                membership.HistoryUnit3 = membershipEditVM.HistoryUnit3;
                membership.HistoryJobTitle3 = membershipEditVM.HistoryJobTitle3;
                membership.StartYear3 = membershipEditVM.StartYear3;
                membership.StartMonth3 = membershipEditVM.StartMonth3;
                membership.EndYear3 = membershipEditVM.EndYear3;
                membership.EndMonth3 = membershipEditVM.EndMonth3;
                membership.TotalYear = membershipEditVM.TotalYear;
                membership.TotalMonth = membershipEditVM.TotalMonth;
                membership.CreateTime = membershipEditVM.CreateTime;
                membership.LastUpdateTime = DateTime.Now;

                if (membershipEditVM.IsValid == true && File == null && String.IsNullOrEmpty(membership.CertificatePath) == true)
                {
                    ViewBag.FileAlert = "<i class=\"fa fa-exclamation-circle\"></i> 請上傳會員證影本";
                    return View();
                }
                //會員證檔案處理及路徑設定
                if (File != null && File.ContentLength > 0)
                {
                    // 取得檔案名稱
                    var fileName = Path.GetFileName(File.FileName);

                    // 指定存放檔案的路徑
                    var path = Path.Combine(Server.MapPath("~/CertificateFiles"), fileName);

                    // 將檔案保存到指定路徑
                    File.SaveAs(path);

                    // 設定封面照片的 URL
                    membership.CertificatePath = Url.Content(Path.Combine("~/CertificateFiles", fileName));
                }
                db.Entry(membership).State = EntityState.Modified;
                db.SaveChanges();
                if (!String.IsNullOrEmpty(membershipEditVM.NewPassword))
                {
                    return RedirectToAction("Login");
                }
                return View(membershipEditVM);
            }

            return View(membershipEditVM);
        }

        [MembershipLoginFilter]
        public ActionResult NewPost()
        {
            return View();
        }

        [MembershipLoginFilter]
        [HttpPost]
        public ActionResult NewPost([Bind(Include = "Title,Content")] Comment comment)
        {
            Comment newComment = new Comment()
            {
                Title = comment.Title,
                Content = comment.Content,
                MembershipId = Convert.ToInt32(Session["membershipId"])
            };
            db.CommentEntities.Add(newComment);
            db.SaveChanges();
            return RedirectToAction("Board");
        }

        [MembershipLoginFilter]
        public ActionResult Post(int? id, int? page)
        {
            Comment comment = db.CommentEntities.Find(id);
            if (comment == null)
            {
                return RedirectToAction("Board");
            }
            CommentRepliesVM model = new CommentRepliesVM()
            {
                Comment = comment,
            };

            //設定一頁幾筆
            int DefaultPageSize = 1;

            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            int countOfReplies = comment.Replies.Count();
            ViewBag.Count = countOfReplies;
            if (countOfReplies > 0)
            {
                //返回結果.ToPageList(現在第幾頁,一頁幾筆)
                model.Replies = comment.Replies.OrderByDescending(p => p.CreateTime).ToPagedList(currentPageIndex, DefaultPageSize);
            }

            return View(model);
        }

        [MembershipLoginFilter]
        public ActionResult Reply(int id)
        {
            Comment comment = db.CommentEntities.Find(id);
            if (comment == null)
            {
                return RedirectToAction("Board");
            }
            CommentReplyVM commentReplyVM = new CommentReplyVM()
            {
                Comment = comment,
            };
            return View(commentReplyVM);
        }

        [MembershipLoginFilter]
        [HttpPost]
        public ActionResult Reply(CommentReplyVM commentReplyVM)
        {
            Reply newReply = new Reply()
            {
                CommentId = commentReplyVM.Comment.Id,
                Content = commentReplyVM.Reply.Content,
                MembershipId = Convert.ToInt32(Session["membershipId"])
            };
            db.ReplyEntities.Add(newReply);
            db.SaveChanges();
            return RedirectToAction("Post", "Membership", new { id = commentReplyVM.Comment.Id });
        }

        // GET: Membership
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Membership/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Membership/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Membership/Create
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

        // GET: Membership/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Membership/Edit/5
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

        // GET: Membership/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Membership/Delete/5
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