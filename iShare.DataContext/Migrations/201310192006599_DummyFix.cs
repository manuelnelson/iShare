namespace iShare.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DummyFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Charities", "CategoryId", "dbo.Charities");
            DropIndex("dbo.Charities", new[] { "CategoryId" });
            CreateIndex("dbo.Charities", "CategoryId");
            AddForeignKey("dbo.Charities", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Charities", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Charities", new[] { "CategoryId" });
            CreateIndex("dbo.Charities", "CategoryId");
            AddForeignKey("dbo.Charities", "CategoryId", "dbo.Charities", "Id");
        }
    }
}
