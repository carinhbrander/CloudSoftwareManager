using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SubscriptionController(ILogger<AccountController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    [HttpPost]
    public async Task<ActionResult<string>> Add(SubscriptionOrderContract contract)
    {
        var subscriptionHandler = new SubscriptionHandler(context);
        await subscriptionHandler.Order(contract);
        return "Subscription ordered";
    }
}