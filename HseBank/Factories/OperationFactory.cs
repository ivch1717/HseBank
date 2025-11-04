using HseBank.TypeOperation;
namespace HseBank.Factories;

public class OperationFactory : IOperationFactory
{
    public Operation Create(int id, ITypeOperation typeOperation, int bankAccountId, int amount, DateTime date,
        int categoryId, string description)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("сумма операции должа быть положитльным числом");
        }
        return new Operation(id, typeOperation, bankAccountId, amount, date, categoryId, description);
    }
}