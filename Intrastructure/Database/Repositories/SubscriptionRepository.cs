public class SubscriptionRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task Add(Subscription subscription)
    {
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();
    }
}