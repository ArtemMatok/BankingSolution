# BankingSolution
# Banking Solution Web API

## Project Description
This project is a REST API for a banking application. It provides features such as account management, fund transactions (deposit, withdraw, transfer), and user management. The solution is built using C# with a clean architecture approach.

---

## Technologies Used
- **.NET 8**: For API development.
- **Entity Framework Core**: For database access and migrations.
- **FluentValidation**: For request validation.
- **AutoMapper**: For object mapping.
- **xUnit**: For unit testing.
- **Moq** and **FluentAssertions**: For mocking and test assertions.

---

## Project Structure
- **BankingSolutionWebApi**: Contains the API controllers, middleware, and configuration.
- **BankingSolutionWebApi.Application**: Contains business logic, services, and DTOs.
- **BankingSolutionWebApi.Domain**: Contains core domain entities and constants.
- **BankingSolutionWebApi.Infrastructure**: Contains database context, repository implementations, and migrations.
- **BankingSolutionWebApi.Tests**: Contains unit tests for services.

---

## Prerequisites
- [SQL Server](https://www.microsoft.com/sql-server)
- Git

---

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/your-username/BankingSolutionWebApi.git
```

### 2. Navigate to the project directory
```bash
cd BankingSolutionWebApi
```
### 3. Update the database connection string
Update the ConnectionStrings section in appsettings.json:
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BankingSolution;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
### 4: Apply migrations
Use the following command to apply the migrations and create the database schema:
```bash
dotnet ef database update --project BankingSolutionWebApi.Infrastructure
```
### 5: Run the application
Run the application using this command:
```bash
dotnet run --project BankingSolutionWebApi
```
