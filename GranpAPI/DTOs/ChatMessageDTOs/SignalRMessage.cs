namespace Granp.DTOs
{
    public class SignalRMessage
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}