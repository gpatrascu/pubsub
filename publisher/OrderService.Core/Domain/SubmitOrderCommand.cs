public class SubmitOrderCommand
{
    public string CustomerId { get; set; }
    public List<OrderLine> OrderLines { get; set; }
}