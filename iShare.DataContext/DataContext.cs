using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Charity> Charities { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupUserMapping(modelBuilder);
            SetupCharityMapping(modelBuilder);
            SetupCauseMapping(modelBuilder);
            SetupCategoryMapping(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SetupCauseMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cause>().HasKey(u => u.Id);
            modelBuilder.Entity<Cause>().Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupCategoryMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(u => u.Id);
            modelBuilder.Entity<Category>().Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupCharityMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Charity>().HasKey(u => u.Id);
            modelBuilder.Entity<Charity>().Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupUserMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }


        void IUnitOfWork.Commit()
        {
            base.SaveChanges();
        }


        /// <summary>
        /// Rollback tracked changes. 
        /// </summary>
        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">
        /// Dialect Query 
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// Enumerable results 
        /// </returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        /// Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
    }
}
