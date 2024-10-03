using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    [HttpGet]
    public async Task<ActionResult<List<Account>>> Get()
    {
        var accountHandler = new AccountHandler(accountRepository);
        var customerId = _userManager.GetUserId(User);
        return await accountHandler.GetAccounts(Guid.Parse(customerId));
    }

    [HttpPost]
    public async Task<ActionResult> Add(string accountName)
    {
        var accountHandler = new AccountHandler(accountRepository);
        var customerId = _userManager.GetUserId(User);
        if (String.IsNullOrWhiteSpace(customerId))
        {
            return BadRequest();
        }
        await accountHandler.Add(Guid.Parse(customerId), accountName);
        return Ok();
    }
}
