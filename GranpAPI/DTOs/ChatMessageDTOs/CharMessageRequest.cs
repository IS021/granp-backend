namespace Granp.DTOs
{
    public class ChatMessageRequest
    {
        public string ConnectionId { get; set; } = null!;
        public string To { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}