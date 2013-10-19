using ServiceStack.DataAnnotations;
using iShare.Models.Contract;

namespace iShare.Models
{
    [Alias("Causes")]
    public class Cause : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
