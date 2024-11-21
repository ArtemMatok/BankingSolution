﻿using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.User.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<NewUserDto>> Register(RegisterDto registerDto);
        Task<Result<NewUserDto>> Login(LoginDto loginDto);
    }
}