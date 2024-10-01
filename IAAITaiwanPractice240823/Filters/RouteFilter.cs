using IAAITaiwanPractice240823.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Filters
{
    public class RouteFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DbModel db = new DbModel();

            //當下的controller
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            //當下的action
            string actionName = filterContext.RouteData.Values["action"].ToString();

            //大標
            string subject = "";
            string action = "";
            string subItemSubject = "";
            List<Permission> SubItemList;

            switch (controllerName)
            {
                case "Contact":
                    subject = "聯絡我們";
                    SubItemList = db.PermissionEntities.Where(p => p.ParentId == 21).ToList();
                    break;

                case "Knowledges":
                    subject = "知識庫";
                    SubItemList = db.PermissionEntities.Where(p => p.ParentId == 17).ToList();
                    break;

                case "News":
                    subject = "訊息發佈";
                    SubItemList = db.PermissionEntities.Where(p => p.ParentId == 8).ToList();
                    break;

                case "Membership":
                    subject = "會員專區";
                    if (actionName == "Login")
                    {
                        Permission login = new Permission
                        {
                            Id = -1,
                            Subject = "會員登入",
                            ControllerName = "Membership",
                            ActionName = "Login"
                        };
                        SubItemList = new List<Permission>() { login };
                    }
                    else if (actionName == "Register")
                    {
                        Permission register = new Permission
                        {
                            Id = -1,
                            Subject = "會員註冊",
                            ControllerName = "Membership",
                            ActionName = "Register"
                        };
                        SubItemList = new List<Permission>() { register };
                    }
                    else
                    {
                        Permission board = new Permission
                        {
                            Id = -1,
                            Subject = "討論區",
                            ControllerName = "Membership",
                            ActionName = "Board"
                        };
                        Permission edit = new Permission
                        {
                            Id = -1,
                            Subject = "修改個人資料",
                            ControllerName = "Membership",
                            ActionName = "Edit"
                        };
                        Permission logout = new Permission
                        {
                            Id = -1,
                            Subject = "登出",
                            ControllerName = "Membership",
                            ActionName = "Logout"
                        };
                        SubItemList = new List<Permission>() { board, edit, logout };
                    }

                    break;

                default:
                    //if (actionName == "Board" || actionName == "NewPost" || actionName == "Post" || actionName == "Reply" || actionName == "Edit")
                    //{
                    //    subject = "會員專區";
                    //    Permission board = new Permission
                    //    {
                    //        Id = -1,
                    //        Subject = "討論區",
                    //        ControllerName = "Membership",
                    //        ActionName = "Board"
                    //    };
                    //    Permission edit = new Permission
                    //    {
                    //        Id = -1,
                    //        Subject = "修改個人資料",
                    //        ControllerName = "Membership",
                    //        ActionName = "Edit"
                    //    };
                    //    Permission logout = new Permission
                    //    {
                    //        Id = -1,
                    //        Subject = "登出",
                    //        ControllerName = "Membership",
                    //        ActionName = "Logout"
                    //    };
                    //    SubItemList = new List<Permission>() { board, edit, logout };
                    //}
                    //else if (actionName == "Login")
                    //{
                    //    subject = "會員專區";
                    //    Permission login = new Permission
                    //    {
                    //        Id = -1,
                    //        Subject = "會員登入",
                    //        ControllerName = "Membership",
                    //        ActionName = "Login"
                    //    };
                    //    SubItemList = new List<Permission>() { login };
                    //}
                    //else if (actionName == "Register")
                    //{
                    //    subject = "會員專區";
                    //    Permission register = new Permission
                    //    {
                    //        Id = -1,
                    //        Subject = "會員註冊",
                    //        ControllerName = "Membership",
                    //        ActionName = "Register"
                    //    };
                    //    SubItemList = new List<Permission>() { register };
                    //}
                    //else
                    if (actionName == "Calendar")
                    {
                        subject = "行事曆";
                        Permission calendar = new Permission
                        {
                            Id = -1,
                            Subject = "協會行事曆",
                            ControllerName = "Articles",
                            ActionName = "Calendar"
                        };
                        SubItemList = new List<Permission>() { calendar };
                    }
                    else if (actionName == "Jobs" || actionName == "Licenses" || actionName == "Refer" || actionName == "Survey")
                    {
                        subject = "協會業務";
                        SubItemList = db.PermissionEntities.Where(p => p.ParentId == 12).ToList();
                    }
                    else
                    {
                        subject = "關於我們";
                        SubItemList = db.PermissionEntities.Where(p => p.ParentId == 2).ToList();
                    }
                    break;
            }
            //長側邊欄
            StringBuilder side = new StringBuilder();
            foreach (var item in SubItemList)
            {
                // 建立 UrlHelper，將 HttpContext 傳入
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

                // 使用 UrlHelper 生成 URL
                string url = urlHelper.Action(item.ActionName, controllerName);

                string active = "";
                if (actionName == item.ActionName || actionName == "Details")
                {
                    active = "active";
                    action = item.ActionName;
                    subItemSubject = item.Subject;
                }
                if (item.Subject == "討論區" && (actionName == "Post" || actionName == "NewPost" || actionName == "Reply"))
                {
                    active = "active";
                    action = item.ActionName;
                    subItemSubject = item.Subject;
                }

                //<li class="active"><a href="#">協會介紹</a></li>
                side.Append($"<li class='{active}'><a href='{url}'>{item.Subject}</a></li>");
            }

            filterContext.Controller.ViewBag.Subject = subject;
            filterContext.Controller.ViewBag.Action = action;
            filterContext.Controller.ViewBag.SubItemSubject = subItemSubject;
            filterContext.Controller.ViewBag.Side = side.ToString();
        }
    }
}