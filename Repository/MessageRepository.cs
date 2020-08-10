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
        Task<List<Message>> GetAllSendMessagesByUserIdAsync(Guid id);
    }

    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly SmContext _smContext;

        public MessageRepository(SmContext smContext) : base(smContext)
        {
            _smContext = smContext;
        }

        public async Task<List<Message>> GetAllSendMessagesByUserIdAsync(Guid id)
            => await _smContext.MessageSenders.Where(p => p.UserId == id).Select(s => s.Message).ToListAsync();

        public async Task<Message> GetLastOfMessagesAsync() 
            => await _smContext.Messages.Select(x => x).FirstOrDefaultAsync();
    }
}