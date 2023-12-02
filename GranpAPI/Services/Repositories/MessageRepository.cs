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

        // Mark all messages from a chat as read (except the ones sent by the user)
        public async Task<bool> MarkAsRead(Guid chatId, Guid userId)
        {
            // Messages not sent by the user
            var filter = Builders<Message>.Filter.Eq(x => x.ChatId, chatId) & Builders<Message>.Filter.Ne(x => x.SenderId, userId);
            var update = Builders<Message>.Update.Set(x => x.Read, true);
            var result = await _collection.UpdateManyAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        // public async Task<List<Message>> GetLastByChatId(Guid chatId, int n)
    }
}
