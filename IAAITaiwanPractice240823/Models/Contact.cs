using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAITaiwanPractice240823.Models

{
    public enum GenderType
    {
        女 = 0,
        男 = 1
    }

    public class Contact
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Phone(ErrorMessage = "{0} 格式錯誤")] // 已包含Datatype
        [MaxLength(10)]
        [Display(Name = "連絡電話")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")] // 已包含Datatype
        [MaxLength(200)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "名稱必填")]
        [MaxLength(50)]
        [Display(Name = "詢問標題")]
        public string Title { get; set; }

        [MaxLength(200)]
        [Display(Name = "詢問內容")]
        public string Content { get; set; }
    }
}