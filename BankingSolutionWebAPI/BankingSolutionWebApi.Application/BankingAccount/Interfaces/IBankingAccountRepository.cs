using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAccountEntity = BankingSolutionWebApi.Domain.Entities.BankingAccount;


namespace BankingSolutionWebApi.Application.BankingAccount.Interfaces;

public interface IBankingAccountRepository
{
    Task<Result<BankingAccountEntity>> CreateBankingAccount(BankingAccountEntity bankingAccount);
    Task<Result<List<BankingAccountEntity>>> GetBankingAccountsByUserId(string userId);
    Task<Result<BankingAccountEntity>> GetBankingAccountByCardNumber(string cardNumber);
    Task<PageResultResponse<BankingAccountEntity>> GetAllBankingAccounts(BankingAccountFilter filter);
    Task<bool> IsBankingAccountLinkedToUser(string cardNumber, string userId);
}

