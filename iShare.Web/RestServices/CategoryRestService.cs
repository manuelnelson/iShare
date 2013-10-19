using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using iShare.BusinessLogic.Contracts;
using iShare.Models;

namespace iShare.Web.RestServices
{
    public class CategoryRestService
    {
        [Route("/Categories", "POST")]
        [Route("/Categories", "PUT")]
        [Route("/Categories", "GET")]
        [Route("/Categories", "DELETE")]
        [Route("/Categories")]
        [Route("/Categories/{Id}")]
        public class CategoryDto : IReturn<CategoryDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public string Name { get; set; }
        }

        public class CategoriesService : ServiceBase
        {
            public ICategoryService CategoryService { get; set; } //Injected by IOC

            public object Get(CategoryDto request)
            {
                return CategoryService.GetAll();
            }

            public object Put(CategoryDto request)
            {
                var CategoryEntity = request.TranslateTo<Category>();
                CategoryService.Update(CategoryEntity);
                return CategoryEntity;
            }

            public object Post(CategoryDto request)
            {
                var CategoryEntity = request.TranslateTo<Category>();
                CategoryService.Add(CategoryEntity);
                return CategoryEntity;
            }

            public void Delete(CategoryDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    CategoryService.DeleteAll(request.Ids);
                else
                    CategoryService.Delete(request.Id);
            }
        }

    }

}
