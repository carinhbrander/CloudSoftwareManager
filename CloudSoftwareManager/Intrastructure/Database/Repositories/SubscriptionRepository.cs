using Microsoft.EntityFrameworkCore;

public class SubscriptionRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Subscription> Get(Guid id)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        return subscription;
    }

    public async Task<List<Subscription>> GetSubscriptions(Guid accountId)
    {
        var subscriptions = await _context.Subscriptions.Where(a => a.AccountId == accountId).ToListAsync();
        return subscriptions;
    }

    public async Task Add(Subscription subscription)
    {
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Subscription subscription)
    {
        _context.Entry(subscription).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}