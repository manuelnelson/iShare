namespace iShare.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDonationEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Donation", c => c.Double(nullable: false));
            DropColumn("dbo.Users", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Position", c => c.String());
            DropColumn("dbo.Users", "Donation");
        }
    }
}
