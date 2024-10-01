using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;

namespace IAAITaiwanPractice240823.Models.ViewModel
{
    public class CommentRepliesVM
    {
        public Comment Comment { get; set; }

        public IPagedList<Reply> Replies { get; set; }
    }
}