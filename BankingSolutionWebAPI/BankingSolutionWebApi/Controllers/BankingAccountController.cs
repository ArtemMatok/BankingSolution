using BankingSolutionWebApi.Application.BankingAccount;
using BankingSolutionWebApi.Application.BankingAccount.Interfaces;
using BankingSolutionWebApi.Application.Common.Extensions;
using BankingSolutionWebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolutionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingAccountController(
        IBankingAccountService _bankingAccountService
    ) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBankingAccount()
        {
            var result = await _bankingAccountService.CreateBankingAccount(UserHelpers.GetUserId(HttpContext));

            return result.ToResponse();
        }

        [Authorize]
        [HttpGet("banking-account-by-user")]
        public async Task<IActionResult> GetBankingAccountByUserId()
        {
            var result = await _bankingAccountService.GetBankingAccountsByUserId(UserHelpers.GetUserId(HttpContext));

            return result.ToResponse();
        }

        [HttpGet("banking-account-by-cardNumber/{number}")]
        public async Task<IActionResult> GetBankingAccountByCardNumber(string number)
        {
            var result = await _bankingAccountService.GetBankingAccountByCardNumber(number);

            return result.ToResponse();
        }

        [HttpGet("all-banking-accounts")]
        public async Task<IActionResult> GetAllBankingAccounts([FromQuery]BankingAccountFilter filter)
        {
            var result = await _bankingAccountService.GetAllBankingAccounts(filter);

            return Ok(result);
        }
    }
}
