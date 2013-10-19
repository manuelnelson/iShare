using ServiceStack.DataAnnotations;
using iShare.Models.Contract;

namespace iShare.Models
{
    [Alias("Charities")]
    public class Charity : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int OrgId { get; set; }
        public long CategoryId { get; set; }
        public string Cause { get; set; }
        public string TagLine { get; set; }
        public string Summary { get; set; }
        public int Rating { get; set; }
        public int Score { get; set; }
    }
}
