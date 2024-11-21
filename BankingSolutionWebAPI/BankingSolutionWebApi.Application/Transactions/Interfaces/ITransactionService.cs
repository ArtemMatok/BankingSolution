using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.Transactions.DTOs;
using BankingSolutionWebApi.Application.Transactions.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.Transactions.Interfaces
{
    public interface ITransactionService
    {
        Task<Result<TransactionDepositeFundsDto>> DepositFunds(FundsRequest depositFunds);
        Task<Result<TransactionDepositeFundsDto>> WithDrawFunds(FundsRequest funds, string userId);
        Task<Result<TransactionDepositeFundsDto>> TransferFunds(TransferFundsRequest funds, string userId);
    }
}
