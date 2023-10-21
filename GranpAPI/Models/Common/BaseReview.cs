
namespace Granp.Models.Common
{
    public abstract class BaseReview<TFrom, TTo> : BaseEntity
        where TFrom : BaseUser
        where TTo : BaseUser
    {
        public TFrom From { get; set; } = null!;
        public TTo To { get; set; } = null!;
        public string? Description { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }
}