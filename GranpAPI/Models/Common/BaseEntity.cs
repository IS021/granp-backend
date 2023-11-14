using System.ComponentModel.DataAnnotations;

namespace Granp.Models.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}