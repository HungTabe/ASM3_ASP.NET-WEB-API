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

        [Authorize(Roles = "0")]
        [HttpPut("update-by-id/{id}")]
        public async Task<IActionResult> Update(short id, [FromBody] SystemAccountRequest accountDto)
        {
            try
            {
                var existingAccount = await _accountService.GetByIdAsync(id);
                if (existingAccount == null) return NotFound();

                accountDto.AccountId = id; // Đảm bảo ID không bị thay đổi
                await _accountService.UpdateAsync(accountDto);
                return Ok("Update user acccount data successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "0")]
        [HttpDelete("delete-account-by-admin/{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            try
            {
                var success = await _accountService.DeleteAsync(id);
                if (!success) return BadRequest("Cannot delete account because it is linked to news articles.");
                return Ok("Delete user acccount data successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
