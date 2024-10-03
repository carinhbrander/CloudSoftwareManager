public class SubscriptionHandler(ISubscriptionRepository subscriptionRepository)
{
    private readonly ISubscriptionRepository _subscriptionRepository = subscriptionRepository;

    public async Task Order(SubscriptionOrderContract contract)
    {
        if (contract.SoftwareId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(contract.SoftwareId));
        }

        if (contract.AccountId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(contract.AccountId));
        }

        if (contract.Quantity < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(contract.Quantity));
        }

        if (contract.ValidTo < DateTime.UtcNow)
        {
            throw new ArgumentOutOfRangeException(nameof(contract.ValidTo));
        }

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

        await _subscriptionRepository.Add(subscription);
    }

    public async Task<List<Subscription>> GetSubscriptions(Guid accountId)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptions(accountId);
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

        var subscription = await _subscriptionRepository.Get(subscriptionId);
        subscription.Quantity = quanitity;
        await subscriptionRepository.Update(subscription);
    }

    public async Task Cancel(Guid subscriptionId)
    {
        try
        {
            var ccpApi = new CCPApi();
            ccpApi.CancelSubscription(subscriptionId);
        }
        catch
        {
            throw;
        }

        var subscription = await _subscriptionRepository.Get(subscriptionId);
        subscription.State = SubscriptionState.Cancelled;
        await subscriptionRepository.Update(subscription);
    }

    public async Task Extend(Guid subscriptionId, DateTime newdate)
    {
        var contract = new SubscriptionChangeContract
        {
            SubscriptionId = subscriptionId,
            ValidTo = newdate
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

        var subscription = await _subscriptionRepository.Get(subscriptionId);
        subscription.ValidTo = newdate;
        await subscriptionRepository.Update(subscription);
    }
}