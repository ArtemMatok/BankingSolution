using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.Transactions.Interfaces;
using BankingSolutionWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Infrastructure.Data
{
    public class TransactionRepository(
        ApplicationDbContext _context
    ) : ITransactionRepository
    {
        public async Task<Result<BankingAccount>> DepositFunds(BankingAccount bankingAccount, decimal amount)
        {
            bankingAccount.Balance += amount;

            try
            {
                _context.BankingAccounts.Update(bankingAccount);
                await _context.SaveChangesAsync();

                return Result<BankingAccount>.Success(bankingAccount);
            }
            catch (Exception ex)
            {
                return Result<BankingAccount>.Failure(ex.Message);
            }
        }

        public  async Task<Result<BankingAccount>> TransferFunds(BankingAccount from, BankingAccount to, decimal amount)
        {
            if (from.Balance < amount)
            {
                return Result<BankingAccount>.Failure("Not enough money on your account");
            }

            from.Balance -= amount;
            to.Balance += amount;

            try
            {
                _context.BankingAccounts.Update(from);
                _context.BankingAccounts.Update(to);
                await _context.SaveChangesAsync();

                return Result<BankingAccount>.Success(from);
            }
            catch (Exception ex)
            {
                return Result<BankingAccount>.Failure(ex.Message);
            }
        }

        public async Task<Result<BankingAccount>> WithdrawFunds(BankingAccount bankingAccount, decimal amount)
        {
            if(bankingAccount.Balance < amount)
            {
                return Result<BankingAccount>.Failure("Not enough money on your account");
            }

            bankingAccount.Balance -= amount;
            try
            {
                _context.BankingAccounts.Update(bankingAccount);
                await _context.SaveChangesAsync();

                return Result<BankingAccount>.Success(bankingAccount);
            }
            catch (Exception ex)
            {
                return Result<BankingAccount>.Failure(ex.Message);
            }
        }
    }
}
