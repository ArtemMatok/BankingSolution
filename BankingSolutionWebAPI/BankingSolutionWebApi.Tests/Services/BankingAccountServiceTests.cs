using Xunit;
using Moq;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankingSolutionWebApi.Application.BankingAccount;
using BankingSolutionWebApi.Application.BankingAccount.Interfaces;
using BankingSolutionWebApi.Application.BankingAccount.DTOs;
using BankingSolutionWebApi.Application.Common.Models;
using BankingSolutionWebApi.Domain.Entities;
using AutoMapper;
using BankingSolutionWebApi.Application.User.Interfaces;

public class BankingAccountServiceTests
{
    private readonly Mock<IBankingAccountRepository> _mockBankingAccountRepository;
    private readonly Mock<IAppUserRepository> _mockAppUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly BankingAccountService _service;

    public BankingAccountServiceTests()
    {
        _mockBankingAccountRepository = new Mock<IBankingAccountRepository>();
        _mockAppUserRepository = new Mock<IAppUserRepository>();
        _mockMapper = new Mock<IMapper>();

        _service = new BankingAccountService(
            _mockBankingAccountRepository.Object,
            _mockAppUserRepository.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task CreateBankingAccount_ShouldReturnFailure_WhenUserNotFound()
    {
        var userId = "nonexistent-user";
        _mockAppUserRepository.Setup(repo => repo.UserExists(userId))
            .ReturnsAsync(false);

        var result = await _service.CreateBankingAccount(userId);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("User wasn`t found");
    }

    [Fact]
    public async Task CreateBankingAccount_ShouldReturnSuccess_WhenAccountCreated()
    {
        var userId = "valid-user";
        var bankingAccount = new BankingAccount { UserId = userId };
        var bankingAccountDto = new BankingAccountGetDto (0,"1264 5847 4589 5748", userId);

        _mockAppUserRepository.Setup(repo => repo.UserExists(userId))
            .ReturnsAsync(true);

        _mockBankingAccountRepository.Setup(repo => repo.CreateBankingAccount(It.IsAny<BankingAccount>()))
            .ReturnsAsync(Result<BankingAccount>.Success(bankingAccount));

        _mockMapper.Setup(mapper => mapper.Map<BankingAccountGetDto>(bankingAccount))
            .Returns(bankingAccountDto);

        var result = await _service.CreateBankingAccount(userId);

        result.IsSuccess.Should().BeTrue();
        result.Value.UserId.Should().Be(userId);
    }

    [Fact]
    public async Task GetBankingAccountByCardNumber_ShouldReturnFailure_WhenCardNumberInvalid()
    {
        var invalidCardNumber = "123";

        var result = await _service.GetBankingAccountByCardNumber(invalidCardNumber);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Card number is invalid");
    }

    [Fact]
    public async Task GetBankingAccountByCardNumber_ShouldReturnSuccess_WhenAccountFound()
    {
        var cardNumber = "1234567812345678";
        var formattedCardNumber = "1234 5678 1234 5678";
        var bankingAccount = new BankingAccount { CardNumber = formattedCardNumber };
        var bankingAccountDto = new BankingAccountGetDto (0,formattedCardNumber, "testUserId" );

        _mockBankingAccountRepository.Setup(repo => repo.GetBankingAccountByCardNumber(formattedCardNumber))
            .ReturnsAsync(Result<BankingAccount>.Success(bankingAccount));

        _mockMapper.Setup(mapper => mapper.Map<BankingAccountGetDto>(bankingAccount))
            .Returns(bankingAccountDto);

        // Act
        var result = await _service.GetBankingAccountByCardNumber(cardNumber);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.CardNumber.Should().Be(formattedCardNumber);
    }

    [Fact]
    public async Task GetBankingAccountsByUserId_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var userId = "nonexistent-user";

        _mockAppUserRepository.Setup(repo => repo.UserExists(userId))
            .ReturnsAsync(false);

        // Act
        var result = await _service.GetBankingAccountsByUserId(userId);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("User wasn`t found");
    }

    [Fact]
    public async Task GetBankingAccountsByUserId_ShouldReturnSuccess_WhenAccountsFound()
    {
        // Arrange
        var userId = "valid-user";
        var bankingAccounts = new List<BankingAccount>
        {
            new BankingAccount { UserId = userId }
        };
        var bankingAccountDtos = new List<BankingAccountGetDto>
        {
            new BankingAccountGetDto( 0,"1234 5687 4569 1233",userId )
        };

        _mockAppUserRepository.Setup(repo => repo.UserExists(userId))
            .ReturnsAsync(true);

        _mockBankingAccountRepository.Setup(repo => repo.GetBankingAccountsByUserId(userId))
            .ReturnsAsync(Result<List<BankingAccount>>.Success(bankingAccounts));

        _mockMapper.Setup(mapper => mapper.Map<List<BankingAccountGetDto>>(bankingAccounts))
            .Returns(bankingAccountDtos);

        // Act
        var result = await _service.GetBankingAccountsByUserId(userId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(1);
        result.Value[0].UserId.Should().Be(userId);
    }
}
