using BankingSolutionWebApi.Application.BankingAccount.DTOs;
using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.BankingAccount.Interfaces
{
    public interface IBankingAccountService
    {
        Task<Result<BankingAccountGetDto>> CreateBankingAccount(string userId);
        Task<Result<List<BankingAccountGetDto>>> GetBankingAccountsByUserId(string userId);
        Task<Result<BankingAccountGetDto>> GetBankingAccountByCardNumber(string cardNumber);
        Task<PageResultResponse<BankingAccountGetDto>> GetAllBankingAccounts(BankingAccountFilter filter);
    }
}
