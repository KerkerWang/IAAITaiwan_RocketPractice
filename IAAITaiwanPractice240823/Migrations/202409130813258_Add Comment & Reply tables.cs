namespace IAAITaiwanPractice240823.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentReplytables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        MembershipId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Memberships", t => t.MembershipId)
                .Index(t => t.MembershipId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        MembershipId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Memberships", t => t.MembershipId)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId)
                .Index(t => t.MembershipId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Replies", "MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.Comments", "MembershipId", "dbo.Memberships");
            DropIndex("dbo.Replies", new[] { "MembershipId" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "MembershipId" });
            DropTable("dbo.Replies");
            DropTable("dbo.Comments");
        }
    }
}
