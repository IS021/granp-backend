namespace GranpAPI.DTOs
{
    public class SignalRMessage
    {
        public string ConnectionId { get; set; } = null!;
        public Guid From { get; set; }
        public string Content { get; set; } = null!;
        public bool Read { get; set; } // TODO: Remove this?
        public DateTime Time { get; set; }
    }

}