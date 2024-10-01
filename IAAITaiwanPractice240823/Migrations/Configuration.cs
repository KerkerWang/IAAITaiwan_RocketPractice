namespace IAAITaiwanPractice240823.Migrations
{
    using IAAITaiwanPractice240823.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DbModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DbModel context)
        {
            DbModel db = new DbModel();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.MemberEntities.Any(m => m.Account == "admin"))
            {
                string passwordSalt = Utility.CreateSalt(5);
                Member admin = new Member
                {
                    Account = "admin",
                    Password = Utility.CreatePasswordHash("admin", passwordSalt), // 注意：應使用雜湊處理密碼
                    PasswordSalt = passwordSalt, // 產生鹽值並加密密碼
                    Name = "Administrator",
                    Email = "admin@example.com",
                    Permission = "A01,B01,B02,B03,B04,B05,C01,D01,E01,E02,E03,E04,F01,G01", // 設置權限為最高
                    IsAdmin = true,
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now,
                };

                context.MemberEntities.Add(admin);
                context.SaveChanges();
            }
        }
    }
}