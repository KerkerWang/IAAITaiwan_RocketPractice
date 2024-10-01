using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;

namespace IAAITaiwanPractice240823.Models
{
    public class DbModel : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'DBModel' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'IAAITaiwanPractice240823.Models.DBModel' 資料庫。
        //
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'DBModel' 連接字串。
        public DbModel()
            : base("name=DbModel")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Article> ArticleEntities { get; set; }

        public virtual DbSet<Member> MemberEntities { get; set; }

        public virtual DbSet<Permission> PermissionEntities { get; set; }

        public virtual DbSet<News> NewsEntities { get; set; }

        public virtual DbSet<Knowledge> KnowledgeEntities { get; set; }

        public virtual DbSet<Contact> ContactEntities { get; set; }

        public virtual DbSet<Membership> MembershipEntities { get; set; }

        public virtual DbSet<Comment> CommentEntities { get; set; }

        public virtual DbSet<Reply> ReplyEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>()
                .HasOptional(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreateBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasOptional(m => m.Updater)
                .WithMany()
                .HasForeignKey(m => m.LastUpdateBy)
                .WillCascadeOnDelete(false);

            // 設定 CreateBy 外鍵行為
            modelBuilder.Entity<Article>()
                .HasOptional(a => a.Creator)
                .WithMany()
                .HasForeignKey(a => a.CreateBy)
                .WillCascadeOnDelete(false); // 如果 Member 被刪除，將外鍵設為 null

            // 設定 LastUpdateBy 外鍵行為
            modelBuilder.Entity<Article>()
                .HasOptional(a => a.Updater)
                .WithMany()
                .HasForeignKey(a => a.LastUpdateBy)
                .WillCascadeOnDelete(false); // 如果 Member 被刪除，將外鍵設為 null

            // 設定 CreateBy 外鍵行為
            modelBuilder.Entity<News>()
                .HasOptional(a => a.Creator)
                .WithMany()
                .HasForeignKey(a => a.CreateBy)
                .WillCascadeOnDelete(false); // 如果 Member 被刪除，將外鍵設為 null

            // 設定 LastUpdateBy 外鍵行為
            modelBuilder.Entity<News>()
                .HasOptional(a => a.Updater)
                .WithMany()
                .HasForeignKey(a => a.LastUpdateBy)
                .WillCascadeOnDelete(false); // 如果 Member 被刪除，將外鍵設為 null

            // 設定 CreateBy 外鍵行為
            modelBuilder.Entity<Knowledge>()
                .HasOptional(a => a.Creator)
                .WithMany()
                .HasForeignKey(a => a.CreateBy)
                .WillCascadeOnDelete(false); // 如果 Member 被刪除，將外鍵設為 null

            // 設定 LastUpdateBy 外鍵行為
            modelBuilder.Entity<Knowledge>()
                .HasOptional(a => a.Updater) //定義關聯屬性是可選（Optional）
                .WithMany() //定義關聯的另一端是一對多（WithMany）
                .HasForeignKey(a => a.LastUpdateBy) //設定外鍵（Foreign Key）
                .WillCascadeOnDelete(false); // 如果 Member 被刪除，將外鍵設為 null

            // 設定當 Member 被刪除時，將 Comment 的 MemberId 設為 null
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.PostedBy)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MembershipId)
                .WillCascadeOnDelete(false); // 刪除 Member 時，不會刪除 Comment，並將 MemberId 設為 null (需要手動處理)

            // 設定當 Member 被刪除時，將 Reply 的 MemberId 設為 null
            modelBuilder.Entity<Reply>()
                .HasRequired(r => r.ReplyBy)
                .WithMany()
                .HasForeignKey(r => r.MembershipId)
                .WillCascadeOnDelete(false); // 刪除 Member 時，不會刪除 Reply，並將 MemberId 設為 null (需要手動處理)

            // 設定當 Comment 被刪除時，相關的 Reply 一併刪除
            modelBuilder.Entity<Reply>()
                .HasRequired(r => r.ReplyTo)
                .WithMany(c => c.Replies)
                .HasForeignKey(r => r.CommentId)
                .WillCascadeOnDelete(true); // 刪除 Comment 時，相關的 Reply 也會被刪除

            // 設定初始資料
            //modelBuilder.Entity<Member>().HasData(
            //    new Member
            //    {
            //        Id = 1,  // 請確保這個 ID 不會與資料庫中其他 ID 發生衝突
            //        Account = "admin",
            //        PasswordSalt = "some_salt_value",  // 你應該用一個真正的鹽值
            //        Password = "hashed_password",  // 你應該儲存哈希過的密碼
            //        Name = "系統管理員",
            //        Email = "admin@example.com",
            //        Permission = "Admin",
            //        IsAdmin = true,
            //        CreateTime = DateTime.Now,
            //        LastUpdateTime = DateTime.Now
            //    }
            //);
        }

        public System.Data.Entity.DbSet<IAAITaiwanPractice240823.Models.ViewModel.ArticleVM> ArticleVMs { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}