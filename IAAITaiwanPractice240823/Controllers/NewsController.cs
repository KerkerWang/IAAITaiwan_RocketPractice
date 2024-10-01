using IAAITaiwanPractice240823.Filters;
using IAAITaiwanPractice240823.Models;
using IAAITaiwanPractice240823.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;

namespace IAAITaiwanPractice240823.Controllers
{
    [RouteFilter]
    public class NewsController : Controller
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
            ViewBag.Count = db.NewsEntities.Count();

            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.NewsEntities.OrderByDescending(p => p.PublishDate).ToPagedList(currentPageIndex, DefaultPageSize));

            // 每頁顯示的項目數
            //int pageSize = 15;

            //// 找到15筆項目
            //var news = db.NewsEntities.OrderBy(a => a.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            //int totalNews = db.NewsEntities.Count();
            //var totalPages = (int)Math.Ceiling((double)totalNews / pageSize);

            //var model = new NewsListVM
            //{
            //    News = news,
            //    CurrentPage = page,
            //    TotalPages = totalPages
            //};

            //return View(model);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int id)
        {
            News news = db.NewsEntities.FirstOrDefault(n => n.Id == id);
            return View(news);
        }
    }
}