using BankingSolutionWebApi.Application.BankingAccount.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.Transactions.DTOs
{
    public record TransactionDepositeFundsDto(
        BankingAccountGetDto bankingAccount,
        string TypeOperation
    );
}
