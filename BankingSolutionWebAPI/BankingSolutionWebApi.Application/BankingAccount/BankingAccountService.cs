using AutoMapper;
using BankingSolutionWebApi.Application.BankingAccount.DTOs;
using BankingSolutionWebApi.Application.BankingAccount.Interfaces;
using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.Common.Response;
using BankingSolutionWebApi.Application.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAccountEntity = BankingSolutionWebApi.Domain.Entities.BankingAccount;

namespace BankingSolutionWebApi.Application.BankingAccount
{
    public class BankingAccountService(
        IBankingAccountRepository _bankingAccountRepository,
        IAppUserRepository _appUserRepository,
        IMapper _mapper
    ) : IBankingAccountService
    {
        public async Task<Result<BankingAccountGetDto>> CreateBankingAccount(string userId)
        {
            if (!await _appUserRepository.UserExists(userId))
            {
                return Result<BankingAccountGetDto>.Failure("User wasn`t found");
            }

            var account = new BankingAccountEntity();
            account.UserId = userId;

            var result =  await _bankingAccountRepository.CreateBankingAccount(account);
            if(!result.IsSuccess)
            {
                return Result<BankingAccountGetDto>.Failure(result.ErrorMessage);
            }

            return Result<BankingAccountGetDto>.Success(_mapper.Map<BankingAccountGetDto>(result.Value));
        }

        public async Task<PageResultResponse<BankingAccountGetDto>> GetAllBankingAccounts(BankingAccountFilter filter)
        {
            var result = await _bankingAccountRepository.GetAllBankingAccounts(filter);

            return new PageResultResponse<BankingAccountGetDto>(_mapper.Map<List<BankingAccountGetDto>>(result.Items), result.TotalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<Result<BankingAccountGetDto>> GetBankingAccountByCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 16)
            {
                return Result<BankingAccountGetDto>.Failure("Card number is invalid");
            }

            var formatedCardNumber = FormatCardNumber(cardNumber);

            var result = await _bankingAccountRepository.GetBankingAccountByCardNumber(formatedCardNumber);

            if(!result.IsSuccess)
            {
                return Result<BankingAccountGetDto>.Failure(result.ErrorMessage);
            }

            return Result<BankingAccountGetDto>.Success(_mapper.Map<BankingAccountGetDto>(result.Value));
        }

        public async Task<Result<List<BankingAccountGetDto>>> GetBankingAccountsByUserId(string userId)
        {
            if(!await _appUserRepository.UserExists(userId))
            {
                return Result<List<BankingAccountGetDto>>.Failure("User wasn`t found");
            }

            var result = await _bankingAccountRepository.GetBankingAccountsByUserId(userId);

            return Result<List<BankingAccountGetDto>>.Success(_mapper.Map<List<BankingAccountGetDto>>(result.Value));
        }

        private string FormatCardNumber(string cardNumber)
        {
            return string.Join(" ", Enumerable.Range(0, 4)
                .Select(i => cardNumber.Substring(i * 4, 4)));
        }
    }
}
