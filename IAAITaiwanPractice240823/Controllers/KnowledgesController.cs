using IAAITaiwanPractice240823.Filters;
using IAAITaiwanPractice240823.Models;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Controllers
{
    [RouteFilter]
    public class KnowledgesController : Controller
    {
        private DbModel db = new DbModel();

        // GET: News
        public ActionResult Index(int? page)
        {
            //設定一頁幾筆
            int DefaultPageSize = 1;

            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.KnowledgeEntities.Count();

            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.KnowledgeEntities.OrderByDescending(p => p.PublishDate).ToPagedList(currentPageIndex, DefaultPageSize));

            // GET: Articles/Details/5
        }

        public ActionResult Details(int id)
        {
            Knowledge knowledge = db.KnowledgeEntities.FirstOrDefault(n => n.Id == id);
            return View(knowledge);
        }
    }
}