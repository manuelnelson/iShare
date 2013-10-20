namespace iShare.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDonationEntityForReal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CharityId = c.Long(nullable: false),
                        Amount = c.Double(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "UserId", "dbo.Users");
            DropIndex("dbo.Donations", new[] { "UserId" });
            DropTable("dbo.Donations");
        }
    }
}
