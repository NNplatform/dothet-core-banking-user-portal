using Microsoft.AspNetCore.Mvc;
using BankingUserPortal.Models;
using System;

namespace BankingUserPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpPost("deposit/{id}")]
        public IActionResult Deposit(int id, [FromBody] TransactionRequest request)
        {
            try
            {
                var account = Account.GetAccount(id);
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

        [HttpPost("depositWithConversion/{id}")]
        public IActionResult DepositWithConversion(int id, [FromBody] TransactionConversionRequest request)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound(JSResponse<string>.Failure($"Account with ID {id} not found."));

                account.Deposit(request.Amount, request.ConversionRate);
                return Ok(JSResponse<string>.Success(
                    $"Deposited {request.Amount:C} with conversion rate {request.ConversionRate} to account {id}. New balance: {account.CheckBalance(true)}"));
            }
            catch (Exception ex)
            {
                return BadRequest(JSResponse<string>.Failure("Error making deposit with conversion", ex.Message));
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

        [HttpPost("invest/{id}")]
        public IActionResult Invest(int id, [FromBody] TransactionRequest request)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound(JSResponse<string>.Failure($"Account with ID {id} not found."));

                if (account is InvestmentAccount investmentAccount)
                {
                    investmentAccount.Invest(request.Amount);
                    return Ok(JSResponse<string>.Success(
                        $"Invested {request.Amount:C} in account {id}. New balance: {account.CheckBalance(true)}"));
                }
                else
                {
                    return BadRequest(JSResponse<string>.Failure("This account does not support investments."));
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(JSResponse<string>.Failure(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(JSResponse<string>.Failure("Error making investment", ex.Message));
            }
        }

        [HttpPost("investWithType/{id}")]
        public IActionResult InvestWithType(int id, [FromBody] InvestmentRequest request)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound(JSResponse<string>.Failure($"Account with ID {id} not found."));

                if (account is InvestmentAccount investmentAccount)
                {
                    investmentAccount.Invest(request.Amount, request.InvestmentType);
                    return Ok(JSResponse<string>.Success(
                        $"Invested {request.Amount:C} in account {id} with type {request.InvestmentType}. New balance: {account.CheckBalance(true)}"));
                }
                else
                {
                    return BadRequest(JSResponse<string>.Failure("This account does not support investments."));
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(JSResponse<string>.Failure(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(JSResponse<string>.Failure("Error making investment", ex.Message));
            }
        }
    }

    public class TransactionRequest
    {
        public decimal Amount { get; set; }
    }

    public class TransactionConversionRequest : TransactionRequest
    {
        public decimal ConversionRate { get; set; }
    }

    public class InvestmentRequest : TransactionRequest
    {
        public string InvestmentType { get; set; }
    }
}
