using Conch.Shared.Dtos.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Conch.Account.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly AccountDbContext _dbcontext;

        public AccountController(ILogger<AccountController> logger, AccountDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        /// <summary>
        /// 获取账户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<GetAccountOutput>> GetAccountList()
        {
            return await _dbcontext.AccountMaster.Select(c=> new GetAccountOutput() { 
                Id = c.Id,
                Name = c.Name,
                CreateTime = c.CreateTime,
                UpdateTime = c.UpdateTime
            }).ToListAsync();
        }
    }
}
