namespace HseBank.Commands.OperationCommand;

public class OperationRequest
{
    public int BankAccountId { get; set; }
    public int CategoryId { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; } = "";
}