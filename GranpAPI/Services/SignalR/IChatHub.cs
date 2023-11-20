namespace Granp.Services.SignalR
{
    public interface IChatHub
    {
        Task MessageReceived(string message);
    }
}