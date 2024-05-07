public class SubmitOrderCommand
{
    public string CustomerId { get; set; }
    public List<OrderLineSubmitted> OrderLines { get; set; }
}