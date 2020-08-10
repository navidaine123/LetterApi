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

        public async Task<Message> GetLastOfMessagesAsync() 
            => await _smContext.Messages
            .Select(x => x)
            .FirstOrDefaultAsync();
    }
}