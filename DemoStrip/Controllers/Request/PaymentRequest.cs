namespace StripDemo.Controllers.Request
{
    public class PaymentRequest
    {
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
    }
}
