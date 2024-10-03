public class SubscriptionHandler(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task Order(SubscriptionOrderContract contract)
    {
        try
        {
            var ccpApi = new CCPApi();
            ccpApi.OrderSubscription(contract);
        }
        catch
        {
            throw;
        }

        var subscription = new Subscription
        {
            AccountId = contract.AccountId,
            SoftwareId = contract.SoftwareId,
            Quantity = contract.Quantity,
            State = SubscriptionState.Active,
            ValidTo = contract.ValidTo
        };

        var subscriptionRepository = new SubscriptionRepository(context);
        await subscriptionRepository.Add(subscription);
    }

    public async Task<List<Subscription>> GetSubscriptions(Guid accountId)
    {
        var subscriptionRepository = new SubscriptionRepository(context);
        var subscriptions = await subscriptionRepository.GetSubscriptions(accountId);
        return subscriptions;
    }

    public async Task ChangeQuantity(Guid subscriptionId, int quanitity)
    {
        var contract = new SubscriptionChangeContract
        {
            SubscriptionId = subscriptionId,
            Quantity = quanitity
        };
        try
        {
            var ccpApi = new CCPApi();
            ccpApi.ChangeSubscription(contract);
        }
        catch
        {
            throw;
        }

        var subscriptionRepository = new SubscriptionRepository(context);
        var subscription = await subscriptionRepository.Get(subscriptionId);
        subscription.Quantity = quanitity;
        await subscriptionRepository.Update(subscription);
    }
}