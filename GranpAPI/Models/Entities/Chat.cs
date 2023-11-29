using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Granp.Models.Common;

namespace Granp.Models.Entities
{
    public class Chat : BaseEntity
    {
        public List<Guid> Members { get; set; } = new List<Guid>();
    }
}