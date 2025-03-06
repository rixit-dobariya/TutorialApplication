using System.ComponentModel;

namespace PTClassWork.Models
{
    public class TransferModel
    {
        [DisplayName("From Account No: ")]
        public int AccountNo { get; set; }
        [DisplayName("From Account Holder Name: ")]
        public string AccountHolderName { get; set; }
        [DisplayName("To Account No: ")]
        public int ToAccountNo { get; set; }
        [DisplayName("To Account Holder Name: ")]
        public string ToAccountHolderName{get;set;}
        [DisplayName("Amount: ")]
        public double Amount { get; set; }
        [DisplayName("Mode: ")]
        public string Mode { get; set; }
    }
}
