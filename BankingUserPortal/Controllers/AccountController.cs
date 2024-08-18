using Microsoft.AspNetCore.Mvc;
using BankingUserPortal.Models;
using System;
using Microsoft.Extensions.Logging;

namespace BankingUserPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(JSResponse<string>.Success("API is working"));
        }

        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            _logger.LogInformation("Received request to create account: {@Account}", account);
            try
            {
                if (account.AccountType == null || account.Balance == 0)
                {
                    return BadRequest(JSResponse<string>.Failure("AccountType and initial Balance are required."));
                }

                var createdAccount = Account.CreateAccount(new Account(account.AccountType, account.Balance));
                _logger.LogInformation("Created new account with ID: {AccountId}", createdAccount.AccountId);
                return Ok(JSResponse<Account>.Success(createdAccount));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account");
                return BadRequest(JSResponse<string>.Failure("Error creating account", ex.Message));
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult GetAccount(int id)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound(JSResponse<string>.Failure($"Account with ID {id} not found."));

                return Ok(JSResponse<Account>.Success(account));
            }
            catch (Exception ex)
            {
                return BadRequest(JSResponse<string>.Failure("Error retrieving account", ex.Message));
            }
        }

        [HttpPost("deposit/{id}")]
        public IActionResult Deposit(int id, [FromBody] TransactionRequest request)
        {
            try
            {
                var account = Account.GetAccount(id);
                _logger.LogDebug("Account ID {AccountId} is of type {AccountType}", id, account?.GetType().Name);
                if (account == null)
                    return NotFound(JSResponse<string>.Failure($"Account with ID {id} not found."));

                account.Deposit(request.Amount);
                return Ok(JSResponse<string>.Success(
                    $"Deposited {request.Amount:C} to account {id}. New balance: {account.CheckBalance(true)}"));
            }
            catch (Exception ex)
            {
                return BadRequest(JSResponse<string>.Failure("Error making deposit", ex.Message));
            }
        }

        [HttpPost("withdraw/{id}")]
        public IActionResult Withdraw(int id, [FromBody] TransactionRequest request)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound(JSResponse<string>.Failure($"Account with ID {id} not found."));

                account.Withdraw(request.Amount);
                return Ok(JSResponse<string>.Success(
                    $"Withdrawn {request.Amount:C} from account {id}. New balance: {account.CheckBalance(true)}"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(JSResponse<string>.Failure(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(JSResponse<string>.Failure("Error making withdrawal", ex.Message));
            }
        }

        [HttpGet("balance/{id}")]
        public IActionResult CheckBalance(int id)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound(JSResponse<string>.Failure($"Account with ID {id} not found."));

                return Ok(JSResponse<string>.Success($"Account {id} balance: {account.CheckBalance(true)}"));
            }
            catch (Exception ex)
            {
                return BadRequest(JSResponse<string>.Failure("Error checking balance", ex.Message));
            }
        }
    }

    public class TransactionRequest
    {
        public decimal Amount { get; set; }
    }
}
