using AutoMapper;
using BankingSolutionWebApi.Application.BankingAccount.DTOs;
using BankingSolutionWebApi.Application.BankingAccount.Interfaces;
using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.Transactions.DTOs;
using BankingSolutionWebApi.Application.Transactions.Interfaces;
using BankingSolutionWebApi.Application.Transactions.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.Transactions
{
    public class TransactionService(
        ITransactionRepository _transactionRepository,
        IBankingAccountRepository _bankingAccount,
        IMapper _mapper
    ) : ITransactionService
    {
        public async Task<Result<TransactionDepositeFundsDto>> DepositFunds(FundsRequest depositFunds)
        {
            var bankingAccount = await _bankingAccount.GetBankingAccountByCardNumber(FormatCardNumber(depositFunds.CardNumber));
            if(!bankingAccount.IsSuccess)
            {
                return Result<TransactionDepositeFundsDto>.Failure("Banking account wasn`t found");
            }

            var result = await _transactionRepository.DepositFunds(bankingAccount.Value, depositFunds.Amount);

            var transaction = new TransactionDepositeFundsDto(_mapper.Map<BankingAccountGetDto>(bankingAccount.Value), "Deposite funds");

            return Result<TransactionDepositeFundsDto>.Success(transaction);
        }

        public async Task<Result<TransactionDepositeFundsDto>> TransferFunds(TransferFundsRequest funds, string userId)
        {
            var fromBankingAccount = await _bankingAccount.GetBankingAccountByCardNumber(FormatCardNumber(funds.FromCardNumber));
            if (!fromBankingAccount.IsSuccess)
            {
                return Result<TransactionDepositeFundsDto>.Failure("Your Banking account wasn`t found");
            }

            var toBankingAccount = await _bankingAccount.GetBankingAccountByCardNumber(FormatCardNumber(funds.ToCardNumber));
            if (!toBankingAccount.IsSuccess)
            {
                return Result<TransactionDepositeFundsDto>.Failure("The second banking account wasn`t found");
            }

            if (!await _bankingAccount.IsBankingAccountLinkedToUser(FormatCardNumber(funds.FromCardNumber), userId))
            {
                return Result<TransactionDepositeFundsDto>.Failure("You don`t have this cards");
            }

            var result = await _transactionRepository.TransferFunds(fromBankingAccount.Value, toBankingAccount.Value, funds.Amount);

            if (!result.IsSuccess)
            {
                return Result<TransactionDepositeFundsDto>.Failure(result.ErrorMessage);
            }

            var transaction = new TransactionDepositeFundsDto(_mapper.Map<BankingAccountGetDto>(result.Value), "Withdraw funds");

            return Result<TransactionDepositeFundsDto>.Success(transaction);
        }

        public async Task<Result<TransactionDepositeFundsDto>> WithDrawFunds(FundsRequest funds, string userId)
        {
            var bankingAccount = await _bankingAccount.GetBankingAccountByCardNumber(FormatCardNumber(funds.CardNumber));
            if (!bankingAccount.IsSuccess)
            {
                return Result<TransactionDepositeFundsDto>.Failure("Banking account wasn`t found");
            }

            if (!await _bankingAccount.IsBankingAccountLinkedToUser(FormatCardNumber(funds.CardNumber),userId))
            {
                return Result<TransactionDepositeFundsDto>.Failure("You don`t have this cards");
            }

            var result = await _transactionRepository.WithdrawFunds(bankingAccount.Value, funds.Amount);

            if(!result.IsSuccess)
            {
                return Result<TransactionDepositeFundsDto>.Failure(result.ErrorMessage);
            }

            var transaction = new TransactionDepositeFundsDto(_mapper.Map<BankingAccountGetDto>(result.Value), "Withdraw funds") ;

            return Result<TransactionDepositeFundsDto>.Success(transaction);
        }

        private string FormatCardNumber(string cardNumber)
        {
            return string.Join(" ", Enumerable.Range(0, 4)
                .Select(i => cardNumber.Substring(i * 4, 4)));
        }
    }
}
