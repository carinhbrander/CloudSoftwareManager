using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SubscriptionController(ILogger<AccountController> logger, ISubscriptionRepository subscriptionRepository, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly ISubscriptionRepository _subscriptionRepository = subscriptionRepository;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    [HttpGet]
    public async Task<ActionResult<List<Subscription>>> Get(Guid accountId)
    {
        var subscriptionHandler = new SubscriptionHandler(_subscriptionRepository);
        return await subscriptionHandler.GetSubscriptions(accountId);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Add(SubscriptionOrderContract contract)
    {
        var subscriptionHandler = new SubscriptionHandler(_subscriptionRepository);
        await subscriptionHandler.Order(contract);
        return "Subscription ordered";
    }

    [HttpPut("changequantity")]
    public async Task<string> ChangeQuantity(Guid subscriptionId, int quanitity)
    {
        var subscriptionHandler = new SubscriptionHandler(_subscriptionRepository);
        await subscriptionHandler.ChangeQuantity(subscriptionId, quanitity);
        return $"changed to {quanitity}";
    }

    [HttpDelete]
    public async Task<string> Cancel(Guid id)
    {
        var subscriptionHandler = new SubscriptionHandler(_subscriptionRepository);
        await subscriptionHandler.Cancel(id);
        return $"canceled {id}";
    }

    [HttpPut("extend")]
    public async Task<string> Extend(Guid subscriptionId, DateTime newdate)
    {
        var subscriptionHandler = new SubscriptionHandler(_subscriptionRepository);
        await subscriptionHandler.Extend(subscriptionId, newdate);
        return $"extended to {newdate}";
    }
}