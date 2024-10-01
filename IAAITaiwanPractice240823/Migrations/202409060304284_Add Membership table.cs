namespace IAAITaiwanPractice240823.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershiptable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        PasswordSalt = c.String(maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        TelPhone = c.String(nullable: false, maxLength: 10),
                        MobilePhone = c.String(nullable: false, maxLength: 10),
                        Address = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 200),
                        IsValid = c.Boolean(nullable: false),
                        CertificatePath = c.String(),
                        Unit = c.String(nullable: false, maxLength: 50),
                        JobTitle = c.String(nullable: false, maxLength: 20),
                        Educational = c.String(nullable: false, maxLength: 50),
                        HistoryUnit1 = c.String(nullable: false, maxLength: 50),
                        HistoryJobTitle1 = c.String(nullable: false, maxLength: 50),
                        StartYear1 = c.String(nullable: false, maxLength: 4),
                        StartMonth1 = c.String(nullable: false, maxLength: 2),
                        EndYear1 = c.String(nullable: false, maxLength: 4),
                        EndMonth1 = c.String(nullable: false, maxLength: 2),
                        HistoryUnit2 = c.String(maxLength: 50),
                        HistoryJobTitle2 = c.String(maxLength: 50),
                        StartYear2 = c.String(maxLength: 4),
                        StartMonth2 = c.String(maxLength: 2),
                        EndYear2 = c.String(maxLength: 4),
                        EndMonth2 = c.String(maxLength: 2),
                        HistoryUnit3 = c.String(maxLength: 50),
                        HistoryJobTitle3 = c.String(maxLength: 50),
                        StartYear3 = c.String(maxLength: 4),
                        StartMonth3 = c.String(maxLength: 2),
                        EndYear3 = c.String(maxLength: 4),
                        EndMonth3 = c.String(maxLength: 2),
                        TotalYear = c.String(nullable: false, maxLength: 2),
                        TotalMonth = c.String(nullable: false, maxLength: 2),
                        CreateTime = c.DateTime(nullable: false),
                        LastUpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Memberships");
        }
    }
}
