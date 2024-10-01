using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAITaiwanPractice240823.Models.ViewModel
{
    public class MembershipEditVM
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [MaxLength(100)]
        [Display(Name = "密碼鹽")]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "'新密碼")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性別")]
        [Required(ErrorMessage = "請選擇性別")]
        public GenderType? Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/M/d}", ApplyFormatInEditMode = true)]
        [Display(Name = "生日")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "請選擇申請類別")]
        [Display(Name = "申請類別")]
        public MemberType? Type { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Phone(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(10)]
        [Display(Name = "連絡電話(公)")]
        public string TelPhone { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Phone(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(10)]
        [Display(Name = "手機")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "通訊處")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")] // 已包含Datatype
        [MaxLength(200)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "是否為有效會員")]
        public bool IsValid { get; set; }

        [Display(Name = "會員證路徑")]
        public string CertificatePath { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "現職單位")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        [Display(Name = "職稱")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "最高學歷")]
        public string Educational { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "服務單位")]
        public string HistoryUnit1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "職稱")]
        public string HistoryJobTitle1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(4)]
        [Display(Name = "起始年")]
        public string StartYear1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(2)]
        [Display(Name = "起始月")]
        public string StartMonth1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(4)]
        [Display(Name = "結束年")]
        public string EndYear1 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(2)]
        [Display(Name = "結束月")]
        public string EndMonth1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "服務單位")]
        public string HistoryUnit2 { get; set; }

        [MaxLength(50)]
        [Display(Name = "職稱")]
        public string HistoryJobTitle2 { get; set; }

        [MaxLength(4)]
        [Display(Name = "起始年")]
        public string StartYear2 { get; set; }

        [MaxLength(2)]
        [Display(Name = "起始月")]
        public string StartMonth2 { get; set; }

        [MaxLength(4)]
        [Display(Name = "結束年")]
        public string EndYear2 { get; set; }

        [MaxLength(2)]
        [Display(Name = "結束月")]
        public string EndMonth2 { get; set; }

        [MaxLength(50)]
        [Display(Name = "服務單位")]
        public string HistoryUnit3 { get; set; }

        [MaxLength(50)]
        [Display(Name = "職稱")]
        public string HistoryJobTitle3 { get; set; }

        [MaxLength(4)]
        [Display(Name = "起始年")]
        public string StartYear3 { get; set; }

        [MaxLength(2)]
        [Display(Name = "起始月")]
        public string StartMonth3 { get; set; }

        [MaxLength(4)]
        [Display(Name = "結束年")]
        public string EndYear3 { get; set; }

        [MaxLength(2)]
        [Display(Name = "結束月")]
        public string EndMonth3 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(2)]
        [Display(Name = "合計年資(年)")]
        public string TotalYear { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(2)]
        [Display(Name = "合計年資(月)")]
        public string TotalMonth { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "創建時間")]
        public DateTime CreateTime { get; set; }
    }
}