using ServiceStack.DataAnnotations;
using iShare.Models.Contract;

namespace iShare.Models
{
    [Alias("Users")]
    public class User : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public int UserAuthId { get; set; }
        public double Donation { get; set; }
    }
}
