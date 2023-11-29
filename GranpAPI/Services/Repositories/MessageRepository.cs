using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Granp.Services.Repositories.Interfaces;
using Granp.Models.Types;
using Granp.Models.Entities;
using Granp.Data;


namespace Granp.Services.Repositories
{
    public class MessageRepository : GenericMongoDbRepository<Message>, IMessageRepository
    {
        public MessageRepository(MongoDbContext mongoDbContext) : base(mongoDbContext, "Messages") { }

        public async Task<List<Message>> GetByChatId(Guid chatId)
        {
            return await _collection.Find(x => x.ChatId == chatId).ToListAsync();
        }

        public async Task<Message> GetLastByChatId(Guid chatId)
        {
            return await _collection.Find(x => x.ChatId == chatId).SortByDescending(x => x.Time).FirstOrDefaultAsync();
        }

        public async Task<int> GetUnreadCount(Guid chatId, Guid userId)
        {
            return (int) await _collection.Find(x => x.ChatId == chatId && x.SenderId != userId && x.Read == false).CountDocumentsAsync();
        }

        // public async Task<List<Message>> GetLastByChatId(Guid chatId, int n)
    }
}
