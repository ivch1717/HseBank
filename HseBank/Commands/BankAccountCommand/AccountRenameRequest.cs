namespace HseBank.Commands.BankAccountCommand;

public class AccountRenameRequest
{
    public int Id { get; set; }
    public string NewName { get; set; }
}