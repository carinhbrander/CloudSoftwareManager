public class CCPApi
{
    public IEnumerable<SoftwareContract> ListSoftware()
    {
        return new List<SoftwareContract>{
            new SoftwareContract {
                Id = Guid.Parse("e6a631e4-32e1-48a3-8f28-0422443f069a"),
                Name = "Microsoft 365"
            },
            new SoftwareContract {
                Id = Guid.Parse("06876f5f-2588-43fe-94c4-4b55b7f03992"),
                Name = "Azure"
            },
            new SoftwareContract {
                Id = Guid.Parse("e0b3680f-a2c7-4d4a-893c-e9e3b9a66b9c"),
                Name = "Dynamics"
            }
        };
    }

    public void OrderSubscription(SubscriptionOrderContract contract)
    {
        var success = true;
        if (!success)
        {
            throw new BadHttpRequestException("Api is down, try again later");
        }
    }
}