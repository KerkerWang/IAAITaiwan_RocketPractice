using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Filters
{
    public class MembershipLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 檢查 Session 中的登入狀態
            var session = filterContext.HttpContext.Session;

            // 檢查 Session 中的登入狀態
            if (session["IsLoggedIn"] == null || (bool)session["IsLoggedIn"] == false)
            {
                // 如果使用者未登入，重導至登入頁面
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                    { "controller", "Membership" },
                    { "action", "Login" }
                    });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}