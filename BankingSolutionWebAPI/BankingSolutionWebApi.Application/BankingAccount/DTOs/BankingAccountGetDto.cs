using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.BankingAccount.DTOs
{
    public record BankingAccountGetDto(
        decimal Balance,
        string CardNumber,
        string UserId
    );
}
