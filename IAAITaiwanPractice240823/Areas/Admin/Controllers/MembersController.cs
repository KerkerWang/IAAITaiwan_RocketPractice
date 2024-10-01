using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IAAITaiwanPractice240823.Areas.Admin.Filters;
using IAAITaiwanPractice240823.Models.ViewModel;
using IAAITaiwanPractice240823.Models;

namespace IAAITaiwanPractice240823.Areas.Admin.Controllers
{
    [Authorize]
    [PermissionFilter]
    public class MembersController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/Members
        public ActionResult Index()
        {
            var memberEntities = db.MemberEntities.Include(m => m.Creator).Include(m => m.Updater);
            return View(memberEntities.ToList());
        }

        // GET: Admin/Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.MemberEntities.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Admin/Members/Create
        public ActionResult Create()
        {
            //ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account");
            //ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account");
            ViewBag.Tree = GetTree();
            return View();
        }

        private string GetTree()
        {
            //組字串
            StringBuilder tree = new StringBuilder("[");
            //取得權限
            var permission = db.PermissionEntities.ToList();
            //第一層權限
            var firstPermissions = permission.Where(p => p.ParentId == null).ToList();
            //使用遞迴組字串
            tree.Append(GetTreeString(firstPermissions));
            //foreach (var p in firstPermissions)
            //{
            //    tree.Append("{");
            //    tree.Append($"\"id\": \"{p.Code}\",");
            //    tree.Append($"\"text\": \"{p.Subject}\"");
            //    if (p.ChildPermissions.Any())

            //    tree.Append("}");
            //}

            tree.Append("]");
            return tree.ToString();
        }

        private string GetTreeString(ICollection<Permission> firstPermission)
        {
            //組字串
            StringBuilder sb = new StringBuilder();

            foreach (var p in firstPermission)
            {
                sb.Append("{");
                sb.Append($"'id': '{p.Code}',");
                sb.Append($"'text': '{p.Subject}',");
                if (p.ChildPermissions.Any())
                {
                    sb.Append("'children':[");
                    sb.Append(GetTreeString(p.ChildPermissions));
                    sb.Append("]");
                }

                sb.Append("},");
            }
            string result = sb.ToString().TrimEnd(',');
            return result;
        }

        // POST: Admin/Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,PasswordSalt,Password,Name,Email,Permission,IsAdmin,CreateTime,CreateBy,LastUpdateTime,LastUpdateBy")] MemberCreateVM memberVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者userId
                int userId = Utility.GetUserData().Id;

                Member member = new Member();
                member.Account = memberVM.Account;
                member.PasswordSalt = Utility.CreateSalt(5);
                member.Password = Utility.CreatePasswordHash(memberVM.Password, member.PasswordSalt);
                member.Name = memberVM.Name;
                member.Email = memberVM.Email;
                member.Permission = memberVM.Permission;
                member.IsAdmin = memberVM.IsAdmin;
                member.CreateBy = userId;
                member.LastUpdateTime = DateTime.Now;
                member.LastUpdateBy = userId;

                db.MemberEntities.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", member.CreateBy);
            //ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", member.LastUpdateBy);
            return View(memberVM);
        }

        // GET: Admin/Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.MemberEntities.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", member.CreateBy);
            //ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", member.LastUpdateBy);
            StringBuilder sb = new StringBuilder("[");
            if (member.Permission != null)
            {
                foreach (string p in member.Permission.Split(','))
                {
                    sb.Append($"'{p}',");
                }
            }

            sb.Append("]");

            MemberEditVM memberEditVM = new MemberEditVM
            {
                Id = member.Id,
                Account = member.Account,
                PasswordSalt = member.PasswordSalt,
                Password = member.Password,
                NewPassword = "",
                Name = member.Name,
                Email = member.Email,
                Permission = member.Permission,
                IsAdmin = member.IsAdmin,
                CreateTime = member.CreateTime,
                CreateBy = member.CreateBy,
                CreateByName = member.Creator?.Name,
                LastUpdateTime = member.LastUpdateTime,
                LastUpdateBy = member.LastUpdateBy,
                LastUpdateByName = member.Updater?.Name,
            };
            ViewBag.Tree = GetTree();
            ViewBag.PermissionTree = $"{sb}";
            return View(memberEditVM);
        }

        // POST: Admin/Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,PasswordSalt,Password,NewPassword,Name,Email,Permission,IsAdmin,CreateTime,CreateBy,LastUpdateTime,LastUpdateBy")] MemberEditVM memberVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者userId
                int userId = Utility.GetUserData().Id;

                Member member = db.MemberEntities.Find(memberVM.Id);
                db.Entry(member).State = EntityState.Modified;
                member.Account = memberVM.Account;
                member.PasswordSalt = memberVM.PasswordSalt;
                member.Password = string.IsNullOrEmpty(memberVM.NewPassword) ? memberVM.Password : memberVM.NewPassword;
                member.Name = memberVM.Name;
                member.Email = memberVM.Email;
                member.Permission = memberVM.Permission;
                member.IsAdmin = memberVM.IsAdmin;
                member.CreateTime = memberVM.CreateTime;
                member.CreateBy = member.CreateBy;
                member.LastUpdateTime = DateTime.Now;
                ///todo LastUpdateBy
                member.LastUpdateBy = userId;

                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    foreach (var validationErrors in ex.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                //        }
                //    }
                //    throw;  // 再次拋出例外以便更上層的代碼可以處理
                //}
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CreateBy = new SelectList(db.MemberEntities, "Id", "Account", member.CreateBy);
            //ViewBag.LastUpdateBy = new SelectList(db.MemberEntities, "Id", "Account", member.LastUpdateBy);
            return View();
        }

        // GET: Admin/Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.MemberEntities.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.MemberEntities.Find(id);
            db.MemberEntities.Remove(member);
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