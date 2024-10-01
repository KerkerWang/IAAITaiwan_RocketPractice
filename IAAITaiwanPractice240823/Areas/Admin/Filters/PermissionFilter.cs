using IAAITaiwanPractice240823.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IAAITaiwanPractice240823.Areas.Admin.Filters
{
    public class PermissionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DbModel db = new DbModel();

            //判斷是否有登入(表單驗證有通過)
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.Side = "";

                return;
            }

            // 取得UserId
            int userId = Utility.GetUserData().Id;

            Member member = db.MemberEntities.Find(userId);
            //判斷是否有此使用者，若無則登出
            if (member == null)
            {
                FormsAuthentication.SignOut();
                return;
            }

            //取得使用者的權限
            string userPermissions = member.Permission;
            //取得符合使用者的資料庫權限資料
            var permissions = db.PermissionEntities.Where(x => userPermissions.Contains(x.Code)).ToList();

            //判斷使用者是否有當下的controller權限
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            //若無則登出
            if (!permissions.Any(x => x.ControllerName == controllerName))
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult("~/Admin/Login/Index");
                return;
            }

            //取得使用者的側邊欄
            string sideBoard = GetSub(permissions, userPermissions, false, controllerName);

            filterContext.Controller.ViewBag.Side = sideBoard;
            filterContext.Controller.ViewBag.userName = Utility.GetUserData().Name;
        }

        private string GetSub(ICollection<Permission> permissions, string userPermission, bool isChild, string controllerName)
        {
            //StringBuilder ulClassContent;
            string ulClassContent = "nav";
            if (isChild == true)
            {
                ulClassContent = "nav flex-column sub-menu";
            }
            StringBuilder sb = new StringBuilder($"<ul class=\"{ulClassContent}\">");
            List<int> parentIdHasRan = new List<int>();
            foreach (var permission in permissions)
            {
                //跑過的permission加到parentIdHasRan裡面
                parentIdHasRan.Add(permission.Id);
                //使用者有該頁面權限
                if (userPermission.Contains(permission.Code))
                {
                    //檢查該權限的parentId沒有被跑過
                    if (permission.ParentId == null || (permission.ParentId != null && !parentIdHasRan.Contains(permission.ParentId.Value)))
                    {
                        //檢查該權限沒有小孩
                        if (!permission.ChildPermissions.Any())
                        {
                            //小孩本人
                            if (isChild == true)
                            {
                                sb.Append($"<li class=\"nav-item\"> <a class=\"nav-link\" href=\"{permission.Url}\">{permission.Subject}</a></li>");
                            }
                            else
                            {
                                sb.Append($"<li class=\"nav-item menu-items\">");
                                sb.Append($"<a class=\"nav-link\" href=\"{permission.Url}\">");
                                sb.Append($"<span class=\"menu-icon\"><i class=\"{permission.Icon}\"></i></span><span class=\"menu-title\">{permission.Subject}</span>");
                                sb.Append("</a>");
                                sb.Append("</li>");
                            }
                        }
                        else
                        {
                            sb.Append($"<li class=\"nav-item menu-items\">");
                            sb.Append($"<a class=\"nav-link\" data-toggle=\"collapse\" href=\"#{permission.Code}\" aria-expanded=\"false\" aria-controls=\"{permission.Code}\">");
                            sb.Append($"<span class=\"menu-icon\"><i class=\"{permission.Icon}\"></i></span><span class=\"menu-title\">{permission.Subject}</span>");
                            sb.Append("<i class=\"menu-arrow\"></i>");
                            sb.Append("</a>");
                            sb.Append($"<div class=\"collapse\" id=\"{permission.Code}\">");
                            sb.Append(GetSub(permission.ChildPermissions, userPermission, true, controllerName));
                            sb.Append("</div>");
                            sb.Append("</li>");
                        }
                    }
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}