namespace Granp.DTOs
{
    public class SignalRMessage
    {
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; } = null!;
        public bool Read { get; set; } // TODO: Remove this?
        public DateTime Time { get; set; }
    }
}