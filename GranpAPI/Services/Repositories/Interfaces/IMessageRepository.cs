using Granp.Models.Entities;
using Granp.Models.Types;

namespace Granp.Services.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<List<Message>> GetByChatId(Guid chatId);
        Task<Message> GetLastByChatId(Guid chatId);
        Task<int> GetUnreadCount(Guid chatId, Guid userId);

        // Get last n messages from a chat
        // Task<List<Message>> GetLastByChatId(Guid chatId, int n);
    }    
}