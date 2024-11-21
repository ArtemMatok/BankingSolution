using AutoMapper;
using BankingSolutionWebApi.Application.BankingAccount.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAccountEntity = BankingSolutionWebApi.Domain.Entities.BankingAccount;

namespace BankingSolutionWebApi.Application.BankingAccount;

public class BankingAccountMapper : Profile
{
    public BankingAccountMapper()
    {
        CreateMap<BankingAccountEntity, BankingAccountGetDto>().ReverseMap() ;
    }
}

