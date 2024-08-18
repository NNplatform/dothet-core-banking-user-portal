using Microsoft.AspNetCore.Mvc;
using BankingUserPortal.Models;
using System;

namespace BankingUserPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            try
            {
                var createdAccount = Account.CreateAccount(account);
                return Ok(createdAccount);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating account: {ex.Message}");
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult GetAccount(int id)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound($"Account with ID {id} not found.");

                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving account: {ex.Message}");
            }
        }

        [HttpPost("deposit/{id}")]
        public IActionResult Deposit(int id, [FromBody] decimal amount)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound($"Account with ID {id} not found.");

                account.Deposit(amount);
                return Ok($"Deposited {amount:C} to account {id}. New balance: {account.CheckBalance(true)}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error making deposit: {ex.Message}");
            }
        }

        [HttpPost("withdraw/{id}")]
        public IActionResult Withdraw(int id, [FromBody] decimal amount)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound($"Account with ID {id} not found.");

                account.Withdraw(amount);
                return Ok($"Withdrawn {amount:C} from account {id}. New balance: {account.CheckBalance(true)}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error making withdrawal: {ex.Message}");
            }
        }

        [HttpGet("balance/{id}")]
        public IActionResult CheckBalance(int id)
        {
            try
            {
                var account = Account.GetAccount(id);
                if (account == null)
                    return NotFound($"Account with ID {id} not found.");

                return Ok($"Account {id} balance: {account.CheckBalance(true)}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error checking balance: {ex.Message}");
            }
        }
    }
}