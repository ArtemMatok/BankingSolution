using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.User.DTOs
{
    public record NewUserDto(string? Email,
        string? Username,
        string? Token
    );
}
