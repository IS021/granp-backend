namespace Granp.DTOs
{
    public class ChatResponse
    {
        public string OtherId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public List<ChatMessageResponse> Messages { get; set; } = null!;
    }
}