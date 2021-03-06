﻿using System.Collections.Generic;
using ServiceStack.OrmLite;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.OrmLiteRepositories
{
    public class CharityOrmLiteRepository : OrmLiteRepository<Charity>, ICharityRepository
    {
        public CharityOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public List<Charity> GetAll()
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                //return db.Select<Charity>();
                return db.SqlList<Charity>("SELECT Id, Name, Url, City, State, OrgId, CategoryId, TagLine, Summary, Rating, Score, Cause FROM Charities");
            }  

        }
    }
}
