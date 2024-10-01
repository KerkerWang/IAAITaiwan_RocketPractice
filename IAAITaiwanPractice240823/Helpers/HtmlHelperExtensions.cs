using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string IsActive(this HtmlHelper html, string controllers, string actions, string cssClass = "active")
        {
            //路由資訊
            var routeData = html.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            //參數資訊
            //var acceptedActions = actions ?? routeAction;
            //var acceptedControllers = controllers ?? routeController;
            var acceptedActions = (actions ?? routeAction).Split(',');
            var acceptedControllers = (controllers ?? routeController).Split(',');

            //比較路由與參數是否相同
            //不相同則回覆空字串
            return acceptedControllers.Contains(routeController) && acceptedActions.Contains(routeAction) ? cssClass : string.Empty;
        }
    }
}