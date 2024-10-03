using Moq;

namespace Tests
{
    public class AccountHandlerTests
    {
        [Fact]
        public async Task Add()
        {
            var accountRepository = new Mock<IAccountRepository>();
            var handler = new AccountHandler(accountRepository.Object);
            var customerId = Guid.NewGuid();
            var accountName = "Testaccount";

            await handler.Add(customerId, accountName);
        }

        [Fact]
        public async Task AddWithoutAccountName()
        {
            var accountRepository = new Mock<IAccountRepository>();
            var handler = new AccountHandler(accountRepository.Object);
            var customerId = Guid.NewGuid();
            var accountName = "";

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Add(customerId, accountName));
        }

        [Fact]
        public async Task GetAccounts()
        {
            var accountRepository = new Mock<IAccountRepository>();

            var mockAccounts = new List<Account> { 
                new Account{
                    Name = "Test"
                }
            };
            accountRepository.Setup(x => x.GetAccounts(It.IsAny<Guid>())).ReturnsAsync(mockAccounts);
            var handler = new AccountHandler(accountRepository.Object);
            var customerId = Guid.NewGuid();
            var accounts = await handler.GetAccounts(customerId);
            Assert.NotNull(accounts);
        }

        [Fact]
        public async Task GetAccountsWithoutCustomerId()
        {
            var accountRepository = new Mock<IAccountRepository>();
            var handler = new AccountHandler(accountRepository.Object);
            var customerId = Guid.Empty;
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.GetAccounts(customerId));
        }
    }
}
