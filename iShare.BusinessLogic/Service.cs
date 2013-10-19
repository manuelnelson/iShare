using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models.Contract;
using Elmah;
using ServiceStack.Common.Web;

namespace iShare.BusinessLogic
{
    public class Service<TRepository, TEntity> : IService<TRepository, TEntity>
        where TRepository : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public TRepository Repository { get; set; }

        public Service(TRepository repository)
        {
            Repository = repository;
        }

        public void Add(TEntity item)
        {
            try
            {
                if (item != null) Repository.Add(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to add item");
            }
        }

        public void AddAll(IEnumerable<TEntity> items)
        {
            try
            {
                Repository.AddAll(items);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to add items");
            }
        }

        public TEntity Get(long id)
        {
            try
            {
                return Repository.Get(id);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to retrieve item");
            }
        }

        public IEnumerable<TEntity> Get(IEnumerable<long> ids)
        {
            try
            {
                return Repository.Get(ids);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to retrieve items");
            }
        }

        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                return Repository.GetFiltered(whereExpression);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to retrieve items");
            }
        }

        public void Update(TEntity item)
        {
            try
            {
                Repository.Update(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to update item");
            }
        }

        public void Delete(TEntity item)
        {
            try
            {
                Repository.Remove(item);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to remove item");
            }
        }

        public void Delete(long id)
        {
            try
            {
                Repository.Remove(id);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to remove item");
            }
        }

        public void DeleteAll(IEnumerable<TEntity> items)
        {
            try
            {
                Repository.RemoveAll(items);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to remove items");
            }
        }

        public void DeleteAll(IEnumerable<long> ids)
        {
            try
            {
                Repository.RemoveAll(ids);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to remove items");
            }
        }

        public void Dispose()
        {
        }
    }
}
