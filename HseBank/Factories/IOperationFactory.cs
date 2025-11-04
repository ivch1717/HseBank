using HseBank.TypeOperation;
namespace HseBank.Factories;

public interface IOperationFactory
{
    public Operation Create(int id, ITypeOperation typeOperation, int bankAccountId, int amount, DateTime date,
        int categoryId, string description = "");
}