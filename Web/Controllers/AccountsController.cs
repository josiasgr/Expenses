using Domain.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly AccountServices accountsService;

        public AccountsController(
            ILogger<AccountsController> logger,
            Services<Account> services
        )
        {
            _logger = logger;
            accountsService = services as AccountServices;
        }

        [HttpGet]
        public Task<IEnumerable<Account>> Get()
        {
            return accountsService.ReadBy(w => true);
        }
    }
}