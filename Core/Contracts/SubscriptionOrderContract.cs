public class SubscriptionOrderContract
{
    public Guid AccountId { get; set; }
    public Guid SoftwareId { get; set; }
    public int Quantity { get; set; }
    public DateTime ValidTo { get; set; }
}