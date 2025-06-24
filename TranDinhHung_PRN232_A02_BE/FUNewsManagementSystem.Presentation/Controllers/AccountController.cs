using FUNewsManagementSystem.Business.IServices;
using FUNewsManagementSystem.Data.DTOs;
using FUNewsManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FUNewsManagementSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelRequest model)
        {
            var account = await _accountService.AuthenticateAsync(model.Email, model.Password);
            if (account == null) return Unauthorized();

            var token = await _accountService.GenerateJwtToken(account);
            return Ok(new { Token = token });
        }

        [Authorize(Roles = "0")]
        [HttpGet("get-all-by-admin")]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }

        [Authorize(Roles = "0")]
        [HttpPost("create-account-by-admin")]
        public async Task<IActionResult> Create([FromBody] SystemAccountRequest account)
        {
            try { 
            await _accountService.CreateAsync(account);
            return CreatedAtAction(nameof(GetAll), account);
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
