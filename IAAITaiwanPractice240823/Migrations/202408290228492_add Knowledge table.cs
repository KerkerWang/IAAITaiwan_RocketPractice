namespace IAAITaiwanPractice240823.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addKnowledgetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Knowledges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublishDate = c.DateTime(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        IsPinned = c.Boolean(nullable: false),
                        Cover = c.String(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Knowledges", "LastUpdateBy", "dbo.Members");
            DropForeignKey("dbo.Knowledges", "CreateBy", "dbo.Members");
            DropIndex("dbo.Knowledges", new[] { "LastUpdateBy" });
            DropIndex("dbo.Knowledges", new[] { "CreateBy" });
            DropTable("dbo.Knowledges");
        }
    }
}
