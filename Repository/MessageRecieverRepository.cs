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
    public interface IMessageRecieverRepository : IGenericRepository<MessageReciever>
    {
        Task<List<MessageReciever>> AddRangeAsync(List<MessageReciever> messageRecievers);

        Task<List<MessageReciever>> GetMessagesRecieveByAync(Guid id);
    }

    public class MessageRecieverRepository : GenericRepository<MessageReciever>, IMessageRecieverRepository
    {
        private readonly SmContext _smContext;

        #region constructors

        public MessageRecieverRepository(SmContext smContext)
            : base(smContext)
        {
            _smContext = smContext;
        }

        #endregion constructors

        public async Task<List<MessageReciever>> AddRangeAsync(List<MessageReciever> messageRecievers)
        {
            await _smContext.MessageRecievers.AddRangeAsync(messageRecievers);

            return messageRecievers;
        }

        public async override Task<MessageReciever> GetAsync(object key)
        {
            return await _smContext
                    .MessageRecievers
                    .Include(x => x.Message)
                    .Include(x => x.MessageSender)
                    .Include(x => x.ResentMessages)
                    .FirstOrDefaultAsync(x => x.Id == (Guid)key);
        }

        public async Task<List<MessageReciever>> GetMessagesRecieveByAync(Guid id)
        {
            try
            {
                var a = await _smContext.MessageRecievers
                    .Include(x => x.Message)
                    .Include(x => x.MessageSender)
                    .Include(x => x.ResentMessages)
                    .Where(x => x.UserId == id)
                    .ToListAsync();

                return a;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}