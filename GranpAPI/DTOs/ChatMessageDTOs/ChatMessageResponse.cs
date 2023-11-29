namespace Granp.DTOs
{
    public class ChatMessageResponse
    {
        public string Sender { get; set; } = null!; // user | other
        public string Content { get; set; } = null!;
        public bool Read { get; set; }
        public DateTime Time { get; set; }
    }
}