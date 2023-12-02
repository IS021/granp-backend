namespace Granp.DTOs
{
    public class ChatMessageRequest
    {
        public string ConnectionId { get; set; } = null!;
        public Guid ChatId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}