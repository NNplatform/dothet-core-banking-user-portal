namespace BankingUserPortal.Models.RequestForms
{
    
    public class TransactionConversionRequest : TransactionRequest
    {
        public decimal ConversionRate { get; set; }
    }

}
