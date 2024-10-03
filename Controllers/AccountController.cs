using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AccountController(ILogger<AccountController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    [HttpGet]
    public async Task<ActionResult<List<Account>>> Get()
    {
        var accountHandler = new AccountHandler(context);
        var customerId = _userManager.GetUserId(User);
        if (String.IsNullOrWhiteSpace(customerId))
        {
            return BadRequest();
        }
        return await accountHandler.GetAccounts(Guid.Parse(customerId));
    }

    [HttpPost("{accountName}")]
    public async Task<ActionResult<string>> Add(string accountName)
    {
        var accountHandler = new AccountHandler(context);
        var customerId = _userManager.GetUserId(User);
        if (String.IsNullOrWhiteSpace(customerId))
        {
            return BadRequest();
        }
        await accountHandler.Add(Guid.Parse(customerId), accountName);
        return "Account created";
    }
}
