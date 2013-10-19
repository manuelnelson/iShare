namespace iShare.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryForeignKeyToCharity : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Charities", "CategoryId");
            AddForeignKey("dbo.Charities", "CategoryId", "dbo.Charities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Charities", "CategoryId", "dbo.Charities");
            DropIndex("dbo.Charities", new[] { "CategoryId" });
        }
    }
}
