namespace IAAITaiwanPractice240823.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentReply : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Replies", "MembershipId", "dbo.Memberships");
            AddColumn("dbo.Replies", "Membership_Id", c => c.Int());
            CreateIndex("dbo.Replies", "Membership_Id");
            AddForeignKey("dbo.Replies", "Membership_Id", "dbo.Memberships", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "Membership_Id", "dbo.Memberships");
            DropIndex("dbo.Replies", new[] { "Membership_Id" });
            DropColumn("dbo.Replies", "Membership_Id");
            AddForeignKey("dbo.Replies", "MembershipId", "dbo.Memberships", "Id");
        }
    }
}
