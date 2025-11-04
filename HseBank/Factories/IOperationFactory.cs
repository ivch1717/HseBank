using HseBank.TypeOperation;
using HseBank.BaseClasses;
namespace HseBank.Factories;

public interface IOperationFactory
{
    public Operation Create(int id, ITypeOperation typeOperation, int bankAccountId, int amount, DateTime date,
        int categoryId, IObserver observer, string description = "");
}