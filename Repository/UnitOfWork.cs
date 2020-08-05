using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork
    {
        void Save();

        Task SaveAsync();

        IUserRepository UserRepository { get; }

        IMessageRepository MessageRepository { get; }

        IMessageSenderRepository MessageSenderRepository { get; }

        MessageRecieverRepository MessageRecieverRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmContext _smContext;

        private IUserRepository userRepository;

        public IUserRepository UserRepository => userRepository ??= new UserRepository(_smContext);

        private IMessageRepository messageRepository;

        public IMessageRepository MessageRepository => messageRepository ??= new MessageRepository(_smContext);

        public IMessageSenderRepository messageSenderRepository;

        public IMessageSenderRepository MessageSenderRepository => messageSenderRepository ??= new MessageSenderRepository(_smContext);

        public MessageRecieverRepository messageRecieverRepository;

        public MessageRecieverRepository MessageRecieverRepository => messageRecieverRepository ??= new MessageRecieverRepository(_smContext);

        public UnitOfWork(SmContext smContext)
        {
            _smContext = smContext;
            //UserRepository = /*new UserRepository(_smContext)*/;
        }

        public void Save()
        {
            _smContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _smContext.SaveChangesAsync();
        }
    }
}