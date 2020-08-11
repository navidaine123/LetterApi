using Microsoft.EntityFrameworkCore;
using Models;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.UserModels;

namespace Repository
{
    public interface IMessageSenderRepository : IGenericRepository<MessageSender>
    {
        Task<List<MessageSender>> GetMessagesSendByAync(Guid id);
    }

    internal class MessageSenderRepository : GenericRepository<MessageSender>, IMessageSenderRepository
    {
        private readonly SmContext _smContext;

        #region constuctors

        public MessageSenderRepository(SmContext smContext)
            : base(smContext)
        {
            _smContext = smContext;
        }

        #endregion constuctors

        public async Task<List<MessageSender>> GetMessagesSendByAync(Guid id)
        {
            try
            {
                var a = await _smContext.MessageSenders
                    .Include(x => x.Message)
                    .Include(x => x.MessageRecievers)
                    .Include(x => x.ResendOn)
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