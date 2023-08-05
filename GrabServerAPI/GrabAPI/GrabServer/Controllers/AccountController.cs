using Azure.Core;
using GrabServer.Services.AccountService;
using GrabServerCore.Common.Constant;
using GrabServerCore.DTOs;
using GrabServerCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrabServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("current-account"),Authorize(Roles = "Driver,User")]
        public async Task<ActionResult<Account>> GetCurrentAccount()
        {
            var result = await _accountService.GetByUsername(User.Identity.Name);
            if (result is null)
                return NotFound("Account not found");

            return Ok(result);
        }

        [HttpGet("username")]
        public async Task<ActionResult<Account>> GetAccountByUsername(string username)
        {
            var result = await _accountService.GetByUsername(username);
            if (result is null)
                return NotFound("Account not found");

            return Ok(result);
        }

        [HttpPut("")]
        public async Task<ActionResult<List<Account>>> UpdateAccount(int id, Account request)
        {
            var result = await _accountService.UpdateAccount(request);
            if (result is null)
                return NotFound("Account not found.");

            return Ok(result);
        }
        [HttpPut("position")]
        public async Task<ActionResult<ResponseMessageDetails<int>>> UpdatePositionAccount(string username, double Long, double Lat)
        {
            var result = await _accountService.UpdatePositionAccount(username, Long, Lat);
            if (result == 0)
                return NotFound("Account not found.");

            return Ok(new ResponseMessageDetails<int>("Update position successfully", result));
        }

    }
}
