namespace IAAITaiwanPractice240823.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControllerName = c.String(nullable: false),
                        ActionName = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateBy = c.Int(),
                        CreateByName = c.String(),
                        LastUpdateTime = c.DateTime(nullable: false),
                        LastUpdateBy = c.Int(),
                        LastUpdateByName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.CreateBy)
                .ForeignKey("dbo.Members", t => t.LastUpdateBy)
                .Index(t => t.CreateBy)
                .Index(t => t.LastUpdateBy);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        PasswordSalt = c.String(maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 200),
                        Permission = c.String(maxLength: 100),
                        IsAdmin = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateBy = c.Int(),
                        CreateByName = c.String(),
                        LastUpdateTime = c.DateTime(nullable: false),
                        LastUpdateBy = c.Int(),
                        LastUpdateByName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.CreateBy)
                .ForeignKey("dbo.Members", t => t.LastUpdateBy)
                .Index(t => t.CreateBy)
                .Index(t => t.LastUpdateBy);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        Code = c.String(maxLength: 5),
                        Url = c.String(maxLength: 500),
                        Icon = c.String(maxLength: 200),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "ParentId", "dbo.Permissions");
            DropForeignKey("dbo.Articles", "LastUpdateBy", "dbo.Members");
            DropForeignKey("dbo.Articles", "CreateBy", "dbo.Members");
            DropForeignKey("dbo.Members", "LastUpdateBy", "dbo.Members");
            DropForeignKey("dbo.Members", "CreateBy", "dbo.Members");
            DropIndex("dbo.Permissions", new[] { "ParentId" });
            DropIndex("dbo.Members", new[] { "LastUpdateBy" });
            DropIndex("dbo.Members", new[] { "CreateBy" });
            DropIndex("dbo.Articles", new[] { "LastUpdateBy" });
            DropIndex("dbo.Articles", new[] { "CreateBy" });
            DropTable("dbo.Permissions");
            DropTable("dbo.Members");
            DropTable("dbo.Articles");
        }
    }
}
