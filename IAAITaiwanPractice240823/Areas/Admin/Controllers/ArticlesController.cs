using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using IAAITaiwanPractice240823.Models.ViewModel;
using IAAITaiwanPractice240823.Models;
using IAAITaiwanPractice240823.Areas.Admin.Filters;

namespace IAAITaiwanPractice240823.Areas.Admin.Controllers
{
    [Authorize]
    [PermissionFilter]
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

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult About([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Organization()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Organization([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult History()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult History([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Member()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Member([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Expert()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Expert([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Jobs()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Jobs([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Licenses()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Licenses([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Refer()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Refer([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Survey()
        {
            // 取得當前 route 的 Controller 名稱
            string controllerName = RouteData.Values["controller"].ToString();
            // 取得當前 route 的 Action 名稱
            string actionName = RouteData.Values["action"].ToString();

            // 找到該route對應的文章內容
            var article = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);

            ArticleVM data = new ArticleVM();
            if (article == null)
            {
                data.ControllerName = controllerName;
                data.ActionName = actionName;
                data.Content = "";
            }
            else
            {
                data.Id = article.Id;
                data.ControllerName = article.ControllerName;
                data.ActionName = article.ActionName;
                data.Content = article.Content;
                data.CreateTime = article.CreateTime;
                data.CreateBy = article.CreateBy;
                data.CreateByName = article.CreateByName;
                data.LastUpdateTime = article.LastUpdateTime;
                data.LastUpdateBy = article.LastUpdateBy;
                data.LastUpdateByName = article.LastUpdateByName;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Survey([Bind(Include = "Id,ControllerName,ActionName,Content,CreateTime,CreateBy,CreateByName,LastUpdateTime,LastUpdateBy,LastUpdateByName")] ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                //取得登入者資訊
                Member editor = Utility.GetUserData();

                //已經有文章
                if (articleVM.Id > 0)
                {
                    Article article = db.ArticleEntities.FirstOrDefault(a => a.Id == articleVM.Id);
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = articleVM.CreateTime.Value;
                    article.CreateBy = articleVM.CreateBy;
                    article.CreateByName = articleVM.CreateByName;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;
                }
                else
                {
                    //沒有文章
                    Article article = new Article();
                    article.ControllerName = articleVM.ControllerName;
                    article.ActionName = articleVM.ActionName;
                    article.Content = articleVM.Content;
                    article.CreateTime = DateTime.Now;
                    article.CreateBy = editor.Id;
                    article.CreateByName = editor.Name;
                    article.LastUpdateTime = DateTime.Now;
                    article.LastUpdateBy = editor.Id;
                    article.LastUpdateByName = editor.Name;

                    db.ArticleEntities.Add(article);
                }
                db.SaveChanges();

                //將新的內容進行綁定並回傳

                // 取得當前 route 的 Controller 名稱
                string controllerName = RouteData.Values["controller"].ToString();
                // 取得當前 route 的 Action 名稱
                string actionName = RouteData.Values["action"].ToString();

                Article newArticle = db.ArticleEntities.FirstOrDefault(a => a.ControllerName == controllerName && a.ActionName == actionName);
                ArticleVM newArticleVM = new ArticleVM()
                {
                    Id = newArticle.Id,
                    ControllerName = newArticle.ControllerName,
                    ActionName = newArticle.ActionName,
                    Content = newArticle.Content,
                    CreateTime = newArticle.CreateTime,
                    CreateBy = newArticle.CreateBy,
                    CreateByName = newArticle.CreateByName,
                    LastUpdateTime = newArticle.LastUpdateTime,
                    LastUpdateBy = newArticle.LastUpdateBy,
                    LastUpdateByName = newArticle.LastUpdateByName
                };
                ModelState.Clear();
                return View(newArticleVM);
            }
            else
            {
                return View();
            }
        }

        //GET: Admin/Articles
        public ActionResult AboutIndex()
        {
            var articleEntities = db.ArticleEntities.Include(m => m.Creator).Include(m => m.Updater);
            //var articleEntities = db.ArticleEntities.Where(a => a.ControllerName == "About");
            return View(articleEntities.ToList());
        }

        // GET: Admin/Articles/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Admin/Articles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Admin/Articles/Create
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

        // GET: Admin/Articles/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Admin/Articles/Edit/5
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

        // GET: Admin/Articles/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Admin/Articles/Delete/5
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