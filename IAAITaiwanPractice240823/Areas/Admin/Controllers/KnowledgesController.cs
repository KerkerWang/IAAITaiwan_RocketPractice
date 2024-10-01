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
    public class KnowledgesController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/Knowledges
        public ActionResult Index()
        {
            var knowledgeEntities = db.KnowledgeEntities.Include(k => k.Creator).Include(k => k.Updater);
            return View(knowledgeEntities.ToList());
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
                var item = db.KnowledgeEntities.Find(Id);
                if (item != null)
                {
                    item.Cover = "/UploadedFiles/" + fileName;
                    db.SaveChanges();
                }
            }

            return Json(new { success = true });
        }

        // GET: Admin/Knowledges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.KnowledgeEntities.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // GET: Admin/Knowledges/Create
        public ActionResult Create()
        {
            ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account");
            ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account");
            return View();
        }

        // POST: Admin/Knowledges/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PublishDate,Title,Content,IsPinned,Cover,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] KnowledgeCreateVM knowledgeVM, HttpPostedFileBase CoverFile)
        {
            if (ModelState.IsValid)
            {
                //取得上傳者資訊
                Member user = Utility.GetUserData();

                Knowledge knowledge = new Knowledge
                {
                    PublishDate = knowledgeVM.PublishDate,
                    Title = knowledgeVM.Title,
                    Content = knowledgeVM.Content,
                    IsPinned = knowledgeVM.IsPinned,
                    CreateBy = user.Id,
                    CreateByName = user.Name,
                    LastUpdateBy = user.Id,
                    LastUpdateByName = user.Name,
                };
                knowledge.LastUpdateTime = knowledge.CreateTime;

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
                    knowledge.Cover = Url.Content(Path.Combine("~/UploadedFiles", fileName));
                }

                db.KnowledgeEntities.Add(knowledge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", knowledge.CreateBy);
            //ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", knowledge.LastUpdateBy);
            return View(knowledgeVM);
        }

        // GET: Admin/Knowledges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.KnowledgeEntities.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", knowledge.CreateBy);
            ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", knowledge.LastUpdateBy);
            return View(knowledge);
        }

        // POST: Admin/Knowledges/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PublishDate,Title,Content,IsPinned,Cover,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knowledge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", knowledge.CreateBy);
            ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", knowledge.LastUpdateBy);
            return View(knowledge);
        }

        // GET: Admin/Knowledges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.KnowledgeEntities.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // POST: Admin/Knowledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knowledge knowledge = db.KnowledgeEntities.Find(id);
            db.KnowledgeEntities.Remove(knowledge);
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