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
        Task<bool> AddRangeAsync(ICollection<MessageReciever> messageRecievers);

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

        public async Task<bool> AddRangeAsync(ICollection<MessageReciever> messageRecievers)
        {
            try
            {
                await _smContext.MessageRecievers.AddRangeAsync(messageRecievers);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MessageReciever>> GetMessagesRecieveByAync(Guid id)
            => await _smContext.MessageRecievers
                .Include(x => x.Message)
                .Include(x => x.MessageSender)
                .Where(x => x.UserId == id)
                .ToListAsync();

        public async Task<string> DeleteFromReciever(MessageReciever messageReciever)
        {
            try
            {
                var message = await _smContext.MessageRecievers.FindAsync(messageReciever.Id);
                message = messageReciever;

                _smContext.MessageRecievers.Update(message);
                await _smContext.SaveChangesAsync();

                return "پیام مورد نظر با موفقیت از لیست حذف شد";
            }
            catch
            {
                return "حذف پیام با خطا روبرو شد";
            }
        }
    }
}