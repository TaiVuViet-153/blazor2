using System.Collections.Generic;

public class WalletVM
{
    public string AccountName { get; set; } = "";
    public string AccountNumber { get; set; } = "";
    public double Balance { get; set; } = 0;
    public List<TransactionVM> TransactionHistory { get; set; }
}