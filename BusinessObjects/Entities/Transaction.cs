namespace BusinessObjects.Entities;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }
    public Guid BidId { get; set; }
    public Bid Bid { get; set; }
}