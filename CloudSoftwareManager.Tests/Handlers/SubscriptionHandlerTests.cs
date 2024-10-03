using Castle.Core.Resource;
using Moq;

namespace Tests
{
    public class SubscriptionHandlerTests
    {
        [Fact]
        public async Task Order()
        {
            var subscriptionRepository = new Mock<ISubscriptionRepository>();
            var handler = new SubscriptionHandler(subscriptionRepository.Object);
            var contract = new SubscriptionOrderContract
            {
                AccountId = Guid.NewGuid(),
                SoftwareId = Guid.NewGuid(),
                Quantity = 1,
                ValidTo = DateTime.UtcNow.AddDays(1)
            };

            await handler.Order(contract);
        }

        [Fact]
        public async Task OrderWithEmptyContract()
        {
            var subscriptionRepository = new Mock<ISubscriptionRepository>();
            var handler = new SubscriptionHandler(subscriptionRepository.Object);
            var contract = new SubscriptionOrderContract();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Order(contract));
        }

        [Fact]
        public async Task OrderWithOutSoftwareId()
        {
            var subscriptionRepository = new Mock<ISubscriptionRepository>();
            var handler = new SubscriptionHandler(subscriptionRepository.Object);
            var contract = new SubscriptionOrderContract
            {
                AccountId = Guid.NewGuid(),
                Quantity = 1,
                ValidTo = DateTime.UtcNow.AddDays(1)
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Order(contract));
        }

        [Fact]
        public async Task OrderWithOutAccountId()
        {
            var subscriptionRepository = new Mock<ISubscriptionRepository>();
            var handler = new SubscriptionHandler(subscriptionRepository.Object);
            var contract = new SubscriptionOrderContract
            {
                SoftwareId = Guid.NewGuid(),
                Quantity = 1,
                ValidTo = DateTime.UtcNow.AddDays(1)
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Order(contract));
        }

        [Fact]
        public async Task OrderWithOutQuantity()
        {
            var subscriptionRepository = new Mock<ISubscriptionRepository>();
            var handler = new SubscriptionHandler(subscriptionRepository.Object);
            var contract = new SubscriptionOrderContract
            {
                SoftwareId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                ValidTo = DateTime.UtcNow.AddDays(1)
            };

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await handler.Order(contract));
        }

        [Fact]
        public async Task OrderWithPastDate()
        {
            var subscriptionRepository = new Mock<ISubscriptionRepository>();
            var handler = new SubscriptionHandler(subscriptionRepository.Object);
            var contract = new SubscriptionOrderContract
            {
                SoftwareId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Quantity = 1,
                ValidTo = DateTime.UtcNow.AddDays(-1)
            };

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await handler.Order(contract));
        }
    }
}
