public class SubscriptionChangeContract
{
    public Guid SubscriptionId { get; set; }
    public int Quantity { get; set; }
    public DateTime ValidTo { get; set; }
    public SubscriptionState State { get; set; }
}