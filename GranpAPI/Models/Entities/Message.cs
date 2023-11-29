using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Granp.Models.Common;

namespace Granp.Models.Entities
{
    public class Message : BaseEntity
    {
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; } = null!;
        public bool Read { get; set; }
        public DateTime Time { get; set; }
    }
}