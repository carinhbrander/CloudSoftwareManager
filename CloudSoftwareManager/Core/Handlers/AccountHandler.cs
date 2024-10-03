public class AccountHandler(IAccountRepository accountRepository)
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task Add(Guid customerId, string accountName)
    {
        if(String.IsNullOrEmpty(accountName))
        {
            throw new ArgumentNullException(nameof(accountName));
        }
        await _accountRepository.Add(customerId, accountName);
    }

    public async Task<List<Account>> GetAccounts(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(customerId));
        }
        var accounts = await _accountRepository.GetAccounts(customerId);
        return accounts;
    }
}