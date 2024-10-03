public class SubscriptionChangeContract
{
    public Guid SubscriptionId { get; set; }
    public int Quantity { get; set; }
    public DateTime ValidTo { get; set; }
}