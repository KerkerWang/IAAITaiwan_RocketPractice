using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAITaiwanPractice240823.Models.ViewModel
{
    public class MemberCreateVM
    {
        //[Key]
        //[Display(Name = "編號")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        //[MaxLength(100)]
        //[Display(Name = "密碼鹽")]
        //public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")] // 已包含Datatype
        [MaxLength(200)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [MaxLength(100)]
        [Display(Name = "權限")]
        public string Permission { get; set; }

        [Display(Name = "是否為管理者")]
        public bool IsAdmin { get; set; }

        //[Display(Name = "創建時間")]
        //public DateTime CreateTime { get; set; } = DateTime.Now;

        //[Display(Name = "創建人")]
        //public int? CreateBy { get; set; }

        //[JsonIgnore]
        //[ForeignKey("CreateBy")]
        //public virtual Member Creator { get; set; }

        //[Display(Name = "最後修改時間")]
        //public DateTime? LastUpdateTime { get; set; }

        //[Display(Name = "最後修改人")]
        //public int? LastUpdateBy { get; set; }

        //[JsonIgnore]
        //[ForeignKey("LastUpdateBy")]
        //public virtual Member Updater { get; set; }
    }
}