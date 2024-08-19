namespace SalesTransactionApp.Models
{
    public class SalesTransactionViewModel
    {
        public int TransactionId { get; set; }
        public string SalesItem { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }
    }
}
