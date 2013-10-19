using System;
using System.Collections.Generic;
using Elmah;
using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic
{
    public class CategoryService : Service<ICategoryRepository, Category>, ICategoryService
    {
        private ICategoryRepository CategoryRepository { get; set; }

        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            CategoryRepository = repository;
        }

        public List<Category> GetAll()
        {
            try
            {
                return CategoryRepository.GetAll();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}
