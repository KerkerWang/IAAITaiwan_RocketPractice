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
    public class Reply
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "文章")]
        public int CommentId { get; set; }

        [JsonIgnore]
        [ForeignKey("CommentId")]
        public virtual Comment ReplyTo { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內容")]
        public string Content { get; set; }

        [Display(Name = "發表人")]
        public int? MembershipId { get; set; }

        [JsonIgnore]
        [ForeignKey("MembershipId")]
        public virtual Membership ReplyBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "發布日期")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}