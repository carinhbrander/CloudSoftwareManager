public class AccountHandler(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task Add(Guid customerId, string accountName)
    {
        var repository = new AccountRepository(_context);
        await repository.Add(customerId, accountName);
    }

    public async Task<List<Account>> GetAccounts(Guid customerId)
    {
        var repository = new AccountRepository(_context);
        var accounts = await repository.GetAccounts(customerId);
        return accounts;
    }
}