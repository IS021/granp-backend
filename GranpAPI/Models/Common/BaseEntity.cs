using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Granp.Models.Common
{
    public abstract class BaseEntity
    {
        [Key]
        [BsonId]
        public Guid Id { get; set; }
    }
}