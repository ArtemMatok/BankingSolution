using BankingSolutionWebApi.Application.BankingAccount;
using BankingSolutionWebApi.Application.BankingAccount.Interfaces;
using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.Common.Response;
using BankingSolutionWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Infrastructure.Data
{
    public class BankingAccountRepository(
        ApplicationDbContext _context    
    ) : IBankingAccountRepository
    {
        public async Task<Result<BankingAccount>> CreateBankingAccount(BankingAccount bankingAccount)
        {
            try
            {
                var res = await _context.AddAsync(bankingAccount);
                await _context.SaveChangesAsync();
                return Result<BankingAccount>.Success(bankingAccount);
            }
            catch (Exception ex)
            {
                return Result<BankingAccount>.Failure(ex.Message);
            }
        }

        public async Task<PageResultResponse<BankingAccount>> GetAllBankingAccounts(BankingAccountFilter filter)
        {
            var accounts = _context.BankingAccounts.AsQueryable();

            int totalCount = await accounts.CountAsync();

            accounts = accounts
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var result  = await accounts.ToListAsync();

            return new PageResultResponse<BankingAccount>(result, totalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<Result<BankingAccount>> GetBankingAccountByCardNumber(string cardNumber)
        {
            var result = await _context.BankingAccounts.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

            if(result == null )
            {
                return Result<BankingAccount>.Failure("Banking account wasn`t found");
            }

            return Result<BankingAccount>.Success(result);
        }

        public async Task<Result<List<BankingAccount>>> GetBankingAccountsByUserId(string userId)
        {
            var result =  await _context.BankingAccounts.Where(x => x.UserId == userId)
                .ToListAsync();

            return Result<List<BankingAccount>>.Success(result);
        }
    }
}
