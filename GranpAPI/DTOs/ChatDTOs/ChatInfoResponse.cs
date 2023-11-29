namespace Granp.DTOs
{
    public class ChatInfoResponse
    {
        public Guid Id { get; set; }
        public string? ProfilePicture { get; set; }
        public string Name { get; set; } = null!;
        public string LastMessage { get; set; } = null!;
        public DateTime Time { get; set; }
        public int UnreadMessages { get; set; }
    }
}