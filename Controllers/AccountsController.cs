using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Endpoint 1: Make a deposit
        /// </summary>
        [HttpPost("deposit")]
        [ProducesResponseType(typeof(DepositResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DepositResponseDto), StatusCodes.Status400BadRequest)]
        public ActionResult<DepositResponseDto> Deposit([FromBody] DepositRequestDto request)
        {
            var response = _accountService.Deposit(request);

            if (!response.Succeeded)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Endpoint 2: Make a withdrawal
        /// </summary>
        [HttpPost("withdraw")]
        [ProducesResponseType(typeof(WithdrawResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(WithdrawResponseDto), StatusCodes.Status400BadRequest)]
        public ActionResult<WithdrawResponseDto> Withdraw([FromBody] WithdrawRequestDto request)
        {
            var response = _accountService.Withdraw(request);

            if (!response.Succeeded)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Endpoint 3: Close an account
        /// </summary>
        [HttpPost("close")]
        [ProducesResponseType(typeof(CloseAccountResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CloseAccountResponseDto), StatusCodes.Status400BadRequest)]
        public ActionResult<CloseAccountResponseDto> CloseAccount([FromBody] CloseAccountRequestDto request)
        {
            var response = _accountService.CloseAccount(request);

            if (!response.Succeeded)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Endpoint 4: Create an account
        /// </summary>
        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateAccountResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateAccountResponseDto), StatusCodes.Status400BadRequest)]
        public ActionResult<CreateAccountResponseDto> CreateAccount([FromBody] CreateAccountRequestDto request)
        {
            var response = _accountService.CreateAccount(request);

            if (!response.Succeeded)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
