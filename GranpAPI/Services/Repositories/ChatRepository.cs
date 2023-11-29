using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Granp.Services.Repositories.Interfaces;
using Granp.Models.Types;
using Granp.Models.Entities;
using Granp.Data;


namespace Granp.Services.Repositories
{
    public class ChatRepository : GenericMongoDbRepository<Chat>, IChatRepository
    {
        public ChatRepository(MongoDbContext mongoDbContext) : base(mongoDbContext, "Chats") { }

        public async Task<List<Chat>> GetByMemberId(Guid memberId)
        {
            return await _collection.Find(x => x.Members.Contains(memberId)).ToListAsync();
        }
    }
}
