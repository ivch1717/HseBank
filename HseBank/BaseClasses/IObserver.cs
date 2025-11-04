using HseBank.TypeOperation;
namespace HseBank.BaseClasses;

public interface IObserver
{
    void Update(ITypeOperation typeOperation, int amount);
}