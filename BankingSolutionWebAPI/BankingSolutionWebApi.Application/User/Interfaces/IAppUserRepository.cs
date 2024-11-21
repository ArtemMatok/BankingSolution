using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.User.Interfaces
{
    public interface IAppUserRepository
    {
        Task<bool> UserExists(string userId);
    }
}
