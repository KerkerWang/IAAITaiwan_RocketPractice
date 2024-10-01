using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAITaiwanPractice240823.Areas.Admin.Filters;
using IAAITaiwanPractice240823.Models;
using IAAITaiwanPractice240823.Models.ViewModel;

namespace IAAITaiwanPractice240823.Areas.Admin.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class NewsController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/News
        public ActionResult Index()
        {
            var newsEntities = db.NewsEntities.Include(n => n.Creator).Include(n => n.Updater);
            return View(newsEntities.ToList());
        }

        [HttpPost]
        public ActionResult UpdateCover(HttpPostedFileBase CoverFile, int Id)
        {
            if (CoverFile != null && CoverFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(CoverFile.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                CoverFile.SaveAs(path);

                // 更新數據庫中該項目的 Cover 字段
                var item = db.NewsEntities.Find(Id);
                if (item != null)
                {
                    item.Cover = "/UploadedFiles/" + fileName;
                    db.SaveChanges();
                }
            }

            return Json(new { success = true });
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsEntities.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account");
            ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account");
            return View();
        }

        // POST: Admin/News/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PublishDate,Title,Content,IsPinned,Cover,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] NewsCreateVM newsVM, HttpPostedFileBase CoverFile)
        {
            if (ModelState.IsValid)
            {
                //取得上傳者資訊
                Member user = Utility.GetUserData();

                News news = new News
                {
                    PublishDate = newsVM.PublishDate,
                    Title = newsVM.Title,
                    Content = newsVM.Content,
                    IsPinned = newsVM.IsPinned,
                    CreateBy = user.Id,
                    CreateByName = user.Name,
                    LastUpdateBy = user.Id,
                    LastUpdateByName = user.Name,
                };
                news.LastUpdateTime = news.CreateTime;

                //封面照片檔案處理及路徑設定
                if (CoverFile != null && CoverFile.ContentLength > 0)
                {
                    // 取得檔案名稱
                    var fileName = Path.GetFileName(CoverFile.FileName);

                    // 指定存放檔案的路徑
                    var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

                    // 將檔案保存到指定路徑
                    CoverFile.SaveAs(path);

                    // 設定封面照片的 URL
                    news.Cover = Url.Content(Path.Combine("~/UploadedFiles", fileName));
                }

                db.NewsEntities.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", news.CreateBy);
            //ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", news.LastUpdateBy);
            return View(newsVM);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsEntities.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", news.CreateBy);
            ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", news.LastUpdateBy);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PublishDate,Title,Content,IsPinned,Cover,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] News newsData)
        {
            if (ModelState.IsValid)
            {
                //取得使用者資訊
                Member user = Utility.GetUserData();

                News news = db.NewsEntities.FirstOrDefault(n => n.Id == newsData.Id);
                db.Entry(news).State = EntityState.Modified;

                news.PublishDate = newsData.PublishDate;
                news.Title = newsData.Title;
                news.Content = newsData.Content;
                news.IsPinned = newsData.IsPinned;
                news.Cover = newsData.Cover;
                news.CreateTime = newsData.CreateTime;
                news.CreateBy = newsData.CreateBy;
                news.CreateByName = newsData.CreateByName;
                news.LastUpdateTime = DateTime.Now;
                news.LastUpdateBy = user.Id;
                news.LastUpdateByName = user.Name;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", news.CreateBy);
            //ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", news.LastUpdateBy);
            return View();
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsEntities.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.NewsEntities.Find(id);
            db.NewsEntities.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}