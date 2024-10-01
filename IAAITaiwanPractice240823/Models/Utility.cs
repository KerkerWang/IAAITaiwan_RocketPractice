using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace IAAITaiwanPractice240823.Models
{
    public class Utility
    {
        #region 密碼加密

        // 建立一個指定長度的隨機salt值
        public static string CreateSalt(int saltLenght)
        {
            //生成一個加密的隨機數
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltLenght];
            rng.GetBytes(buff);

            //返回一個Base64隨機數的字串
            return Convert.ToBase64String(buff);
        }

        // 返回密碼加鹽且雜湊後的字串(密碼 , salt)
        public static string CreatePasswordHash(string pwd, string strSalt)
        {
            //把密碼和Salt連起來
            string saltAndPwd = String.Concat(pwd, strSalt);
            //對密碼進行雜湊
            string hashenPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            //轉為小寫字元並擷取前16個字串
            hashenPwd = hashenPwd.ToLower().Substring(0, 16);
            //返回雜湊後的值
            return hashenPwd;
        }

        #endregion 密碼加密

        #region 表單驗證

        /// <summary>
        /// 設定認證票證並創建用戶的認證 Cookie。
        /// </summary>
        /// <param name="userData">要存儲在認證票證中的用戶特定數據。</param>
        /// <param name="userId">用戶的識別符。</param>
        /// <returns>包含加密認證票證的認證 Cookie。</returns>
        public static HttpCookie SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,                        //票證的版本號碼, 用於未來可能的版本控制
                userId,                   //與票證相關的使用者名稱, 會被用來辨識是哪一個使用者的票證
                DateTime.Now,             //核發此票證時的本機日期和時間, 票證的開始時間
                DateTime.Now.AddHours(3), //票證到期的本機日期和時間, 票證何時失效
                false,                    //是否將票證持續存放於Cookie中
                                          //如果設為 true，即使使用者關閉瀏覽器，票證也會保存在Cookie中直到到期時間。如果設為 false，票證會在瀏覽器關閉時失效。
                userData                  //要與票證一起存放的使用者特定資料
            );
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            //回傳Cookie 。
            return authenticationcookie;
        }

        #endregion 表單驗證

        #region 取得登入者使用者資訊

        public static Member GetUserData()
        {
            // "HttpContext.Current"：目前 HTTP 請求的上下文物件，包含了請求和回應的所有資訊。
            // "Identity" 屬性是 IIdentity 類型，用於描述使用者的身份（如名稱、是否已經驗證等）。
            // 從目前的http請求取得使用者的身份資訊並強制轉型為 FormsIdentity，以便存取 FormsIdentity 特有的屬性和方法。
            FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

            // 從身份票證中提取資訊
            FormsAuthenticationTicket ticket = identity.Ticket;

            // 將 Name 屬性轉換為 userId
            Member userData = JsonConvert.DeserializeObject<Member>(ticket.UserData);

            return userData;

            // 如果使用者未驗證，回傳一個預設值（例如 0 或 -1）
            //return -1;
        }

        #endregion 取得登入者使用者資訊
    }
}