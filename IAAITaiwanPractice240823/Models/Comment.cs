using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace IAAITaiwanPractice240823.Models
{
    public class Comment
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內容")]
        public string Content { get; set; }

        [Display(Name = "發表人")]
        public int? MembershipId { get; set; }

        [JsonIgnore]
        [ForeignKey("MembershipId")]
        public virtual Membership PostedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "發布日期")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public virtual ICollection<Reply> Replies { get; set; } //一個文章可以有多個回覆
    }
}