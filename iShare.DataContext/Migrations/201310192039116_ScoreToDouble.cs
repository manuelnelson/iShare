namespace iShare.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScoreToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Charities", "Score", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Charities", "Score", c => c.Int(nullable: false));
        }
    }
}
