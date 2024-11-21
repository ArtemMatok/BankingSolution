using Xunit;
using Moq;
using FluentAssertions;
using System.Threading.Tasks;
using BankingSolutionWebApi.Application.Transactions;
using BankingSolutionWebApi.Application.Transactions.Interfaces;
using BankingSolutionWebApi.Application.BankingAccount.Interfaces;
using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Application.BankingAccount.DTOs;
using BankingSolutionWebApi.Application.Transactions.DTOs;
using BankingSolutionWebApi.Application.Transactions.Requests;
using BankingSolutionWebApi.Domain.Entities;
using AutoMapper;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _mockTransactionRepository;
    private readonly Mock<IBankingAccountRepository> _mockBankingAccountRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly TransactionService _service;

    public TransactionServiceTests()
    {
        _mockTransactionRepository = new Mock<ITransactionRepository>();
        _mockBankingAccountRepository = new Mock<IBankingAccountRepository>();
        _mockMapper = new Mock<IMapper>();

        _service = new TransactionService(
            _mockTransactionRepository.Object,
            _mockBankingAccountRepository.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task DepositFunds_ShouldReturnFailure_WhenBankingAccountNotFound()
    {
        var request = new FundsRequest { CardNumber = "1234567812345678", Amount = 100 };

        _mockBankingAccountRepository.Setup(repo => repo.GetBankingAccountByCardNumber(It.IsAny<string>()))
            .ReturnsAsync(Result<BankingAccount>.Failure("Banking account wasn`t found"));

        var result = await _service.DepositFunds(request);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Banking account wasn`t found");
    }

    [Fact]
    public async Task DepositFunds_ShouldReturnSuccess_WhenBankingAccountExists()
    {
        var request = new FundsRequest { CardNumber = "1234567812345678", Amount = 100 };
        var bankingAccount = new BankingAccount { CardNumber = "1234567812345678", Balance = 100 };
        var bankingAccountDto = new BankingAccountGetDto (200,"1234567812345678","testId");

        _mockBankingAccountRepository.Setup(repo => repo.GetBankingAccountByCardNumber(It.IsAny<string>()))
            .ReturnsAsync(Result<BankingAccount>.Success(bankingAccount));

        _mockTransactionRepository.Setup(repo => repo.DepositFunds(It.IsAny<BankingAccount>(), It.IsAny<decimal>()))
            .ReturnsAsync(Result<BankingAccount>.Success(bankingAccount));

        _mockMapper.Setup(mapper => mapper.Map<BankingAccountGetDto>(It.IsAny<BankingAccount>()))
            .Returns(bankingAccountDto);

        var result = await _service.DepositFunds(request);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task TransferFunds_ShouldReturnFailure_WhenSourceAccountNotFound()
    {
        var request = new TransferFundsRequest { FromCardNumber = "1111222233334444", ToCardNumber = "5555666677778888", Amount = 50 };
        var userId = "user123";

        _mockBankingAccountRepository.Setup(repo => repo.GetBankingAccountByCardNumber(It.IsAny<string>()))
            .ReturnsAsync(Result<BankingAccount>.Failure("Your Banking account wasn`t found"));

        var result = await _service.TransferFunds(request, userId);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Your Banking account wasn`t found");
    }

    [Fact]
    public async Task WithDrawFunds_ShouldReturnFailure_WhenAccountNotFound()
    {
        var request = new FundsRequest { CardNumber = "1234567812345678", Amount = 50 };
        var userId = "user123";

        _mockBankingAccountRepository.Setup(repo => repo.GetBankingAccountByCardNumber(It.IsAny<string>()))
            .ReturnsAsync(Result<BankingAccount>.Failure("Banking account wasn`t found"));

        var result = await _service.WithDrawFunds(request, userId);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Banking account wasn`t found");
    }

    [Fact]
    public async Task WithDrawFunds_ShouldReturnSuccess_WhenAccountExistsAndSufficientFunds()
    {
        var bankingAccount = new BankingAccount { CardNumber = "1234567812345678", Balance = 100 };
        var request = new FundsRequest { CardNumber = "1234567812345678", Amount = 50 };
        var userId = "user123";
        var bankingAccountDto = new BankingAccountGetDto (50,"1234567812345678", userId);

        _mockBankingAccountRepository.Setup(repo => repo.GetBankingAccountByCardNumber(It.IsAny<string>()))
            .ReturnsAsync(Result<BankingAccount>.Success(bankingAccount));

        _mockBankingAccountRepository.Setup(repo => repo.IsBankingAccountLinkedToUser(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        _mockTransactionRepository.Setup(repo => repo.WithdrawFunds(It.IsAny<BankingAccount>(), It.IsAny<decimal>()))
            .ReturnsAsync(Result<BankingAccount>.Success(bankingAccount));

        _mockMapper.Setup(mapper => mapper.Map<BankingAccountGetDto>(It.IsAny<BankingAccount>()))
            .Returns(bankingAccountDto);

        var result = await _service.WithDrawFunds(request, userId);

        result.IsSuccess.Should().BeTrue();
    }
}
