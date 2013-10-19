namespace iShare.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Categories Values ('Animals');" +
                "Insert into Categories Values ('Arts, Culture, Humanities');" +
                "Insert into Categories Values ('Education');" +
                "Insert into Categories Values ('Environment');" +
                "Insert into Categories Values ('Health');" +
                "Insert into Categories Values ('Human Services');" +
                "Insert into Categories Values ('International');" +
                "Insert into Categories Values ('Public Benefit');" +
                "Insert into Categories Values ('Religion');"
                );

           
        }

        public override void Down()
        {
        }
    }
}
