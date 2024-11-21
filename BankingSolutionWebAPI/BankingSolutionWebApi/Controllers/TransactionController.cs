using BankingSolutionWebApi.Application.Common.Extensions;
using BankingSolutionWebApi.Application.Transactions.Interfaces;
using BankingSolutionWebApi.Application.Transactions.Requests;
using BankingSolutionWebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolutionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(
        ITransactionService _transactionService    
    ) : ControllerBase
    {
        [Authorize]
        [HttpPut("deposide-funds")]
        public async Task<IActionResult> DepositeFund(FundsRequest depositFunds)
        {
            var result = await _transactionService.DepositFunds(depositFunds);

            return result.ToResponse();
        }

        [Authorize]
        [HttpPut("withdraw-funds")]
        public async Task<IActionResult> WithdrawFund(FundsRequest funds)
        {
            var result = await _transactionService.WithDrawFunds(funds, UserHelpers.GetUserId(HttpContext));

            return result.ToResponse();
        }

        [Authorize]
        [HttpPut("transfer")]
        public async Task<IActionResult> Transfer(TransferFundsRequest funds)
        {
            var result = await _transactionService.TransferFunds(funds, UserHelpers.GetUserId(HttpContext));

            return result.ToResponse(); 
        }
    }
}
