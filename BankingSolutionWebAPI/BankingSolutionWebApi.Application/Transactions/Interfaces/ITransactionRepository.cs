using BankingSolutionWebApi.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAccountEntity = BankingSolutionWebApi.Domain.Entities.BankingAccount;

namespace BankingSolutionWebApi.Application.Transactions.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Result<BankingAccountEntity>> DepositFunds(BankingAccountEntity bankingAccount, decimal amount);
        Task<Result<BankingAccountEntity>> WithdrawFunds(BankingAccountEntity bankingAccount, decimal amount);
        Task<Result<BankingAccountEntity>> TransferFunds(BankingAccountEntity from, BankingAccountEntity to, decimal amount);
    }
}
