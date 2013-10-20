using ServiceStack.DataAnnotations;
using iShare.Models.Contract;

namespace iShare.Models
{
    [Alias("Donations")]
    public class Donation : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long CharityId { get; set; }
        public double Amount { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public User User { get; set; }
    }
}
