using ServiceStack.DataAnnotations;
using iShare.Models.Contract;

namespace iShare.Models
{
    [Alias("Categories")]
    public class Category : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
