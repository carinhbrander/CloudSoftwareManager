public class Account
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public required string Name { get; set; }
    public List<Subscription>? Subscriptions { get; set; }
}