using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAITaiwanPractice240823.Models
{
    public class News : ObjectModelBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "發布日期")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內容")]
        public string Content { get; set; }

        [Display(Name = "置頂")]
        public bool IsPinned { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "封面照片")]
        public string Cover { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        //[Display(Name = "創建時間")]
        //public DateTime CreateTime { get; set; } = DateTime.Now;

        //[Display(Name = "創建人")]
        //public int? CreateBy { get; set; }

        //[Display(Name = "創建人姓名")]
        //public string CreateByName { get; set; }  // 新增欄位來儲存創建者姓名

        [JsonIgnore]
        [ForeignKey("CreateBy")]
        public virtual Member Creator { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        //[Display(Name = "最後修改時間")]
        //public DateTime LastUpdateTime { get; set; }

        //[Display(Name = "最後修改人")]
        //public int? LastUpdateBy { get; set; }

        //[Display(Name = "最後修改人姓名")]
        //public string LastUpdateByName { get; set; }  // 新增欄位來儲存最後修改者姓名

        [JsonIgnore]
        [ForeignKey("LastUpdateBy")]
        public virtual Member Updater { get; set; }
    }
}