using Microsoft.EntityFrameworkCore;
using Models;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<Message> GetLastOfMessagesAsync();
    }

    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly SmContext _smContext;

        public MessageRepository(SmContext smContext) : base(smContext)
        {
            _smContext = smContext;
        }

        public override async Task<Message> GetAsync(object key) => await _smContext.Messages
                .Include(x => x.MessageSenders)
                .ThenInclude(x => x.User)
                .Include(x => x.MessageRecievers)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == (Guid)key);

        public async Task<Message> GetLastOfMessagesAsync()
            => await _smContext.Messages
            .Select(x => x)
            .FirstOrDefaultAsync();
    }
}