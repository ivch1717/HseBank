using HseBank.BaseClasses;
namespace HseBank.service;

public interface IBankAccountRepository
{
    public void Add(int id, BankAccount account);
    public void Remove(int id);
    public bool IsEmpty();
    public Dictionary<int, BankAccount> GetRep();
}