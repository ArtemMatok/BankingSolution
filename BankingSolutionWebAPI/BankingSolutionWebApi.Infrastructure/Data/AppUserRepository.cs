using BankingSolutionWebApi.Application.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Infrastructure.Data
{
    public class AppUserRepository(
        ApplicationDbContext _context 
    ) : IAppUserRepository
    {
        public async Task<bool> UserExists(string userId)
        {
            var user = await _context.Users.FindAsync( userId );

            if (user == null) return false;
            return true;
        }
    }
}
