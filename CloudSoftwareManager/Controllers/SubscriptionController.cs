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

    [HttpGet]
    public async Task<ActionResult<List<Subscription>>> Get(Guid accountId)
    {
        var subscriptionHandler = new SubscriptionHandler(context);
        return await subscriptionHandler.GetSubscriptions(accountId);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Add(SubscriptionOrderContract contract)
    {
        var subscriptionHandler = new SubscriptionHandler(context);
        await subscriptionHandler.Order(contract);
        return "Subscription ordered";
    }

    [HttpPut("changequantity")]
    public async Task<string> ChangeQuantity(Guid subscriptionId, int quanitity)
    {
        var subscriptionHandler = new SubscriptionHandler(context);
        await subscriptionHandler.ChangeQuantity(subscriptionId, quanitity);
        return $"changed to {quanitity}";
    }

    [HttpDelete]
    public async Task<string> Cancel(Guid id)
    {
        var subscriptionHandler = new SubscriptionHandler(context);
        await subscriptionHandler.Cancel(id);
        return $"canceled {id}";
    }

    [HttpPut("extend")]
    public async Task<string> Extend(Guid subscriptionId, DateTime newdate)
    {
        var subscriptionHandler = new SubscriptionHandler(context);
        await subscriptionHandler.Extend(subscriptionId, newdate);
        return $"extended to {newdate}";
    }
}