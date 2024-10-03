using Microsoft.EntityFrameworkCore;

public class AccountRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task Add(Guid customerId, string accountName)
    {
        var account = new Account
        {
            CustomerId = customerId,
            Name = accountName
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Account>> GetAccounts(Guid customerId)
    {
        var accounts = await _context.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();
        return accounts;
    }
}