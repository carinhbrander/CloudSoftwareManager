public class Subscription
{
    public Guid Id { get; set; }
    public Guid SoftwareId { get; set; }
    public int Quantity { get; set; }
    public SubscriptionState State { get; set; }
    public DateTime ValidTo { get; set; }
}

public enum SubscriptionState
{
    NotSet = 0,
    Active = 1,
    Cancelled = 2
}