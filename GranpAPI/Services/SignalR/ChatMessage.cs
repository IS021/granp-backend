namespace Granp.Services.SignalR
{
    public class ChatMessage
    {
        public string ConnectionId { get; set; } = null!;
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}