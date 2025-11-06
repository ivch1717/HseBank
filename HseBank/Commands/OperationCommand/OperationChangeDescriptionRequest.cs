using HseBank.Facades;
namespace HseBank.Commands.OperationCommand;

public class OperationChangeDescriptionRequest
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
}