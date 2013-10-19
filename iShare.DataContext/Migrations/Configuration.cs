using System.Data.Entity.Migrations;

namespace iShare.DataContext.Migrations
{
    public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(DataContext context)
        {
            //put seed data here            
            
        }
    }
}
