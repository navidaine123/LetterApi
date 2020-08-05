using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Models.UserModels;

namespace Repository
{

    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUserNameAsync(string userName);
    }



    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SmContext _smContext;

        public UserRepository(SmContext smContext):base(smContext)
        {
            _smContext = smContext;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _smContext.Users.FirstOrDefaultAsync(p => userName.Equals(p.UserName));
        }

        
    }
}
