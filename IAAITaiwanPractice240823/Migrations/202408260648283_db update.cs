namespace IAAITaiwanPractice240823.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControllerName = c.String(nullable: false),
                        ActionName = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreateTime = c.DateTime(),
                        CreateBy = c.Int(),
                        CreateByName = c.String(),
                        LastUpdateTime = c.DateTime(),
                        LastUpdateBy = c.Int(),
                        LastUpdateByName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ArticleVMs");
        }
    }
}
