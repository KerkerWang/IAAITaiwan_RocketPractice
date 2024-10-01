using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAITaiwanPractice240823.Models.ViewModel
{
    public class NewsListVM
    {
        public List<News> News { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}