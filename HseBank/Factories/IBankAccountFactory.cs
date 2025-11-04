using HseBank.BaseClasses;
namespace HseBank.Factories;

public interface IBankAccountFactory
{
    public BankAccount Create(int id, string name, int balance);
}