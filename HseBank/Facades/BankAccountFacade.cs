using HseBank.Factories;
using HseBank.service;

namespace HseBank.Facades;

public class BankAccountFacade : IBankAccountFacade
{
    IBankAccountRepository  _repository;
    IBankAccountFactory _factory;

    public BankAccountFacade(IBankAccountRepository repository,  IBankAccountFactory factory)
    {
        _repository = repository;
        _factory = factory;
    }
    public void AddAccount(string name, int balance)
    {
        int id = _repository.GetRep().Keys.Max() + 1;
        _repository.Add(id, _factory.Create(id, name, balance));
    }

    public void RemoveAccount(int id)
    {
        if (!_repository.GetRep().Keys.Contains(id))
        {
            throw new ArgumentException("id не корректный");
        }
        _repository.Remove(id);
    }

    public void ChangeName(int id, string name)
    {
        if (!_repository.GetRep().Keys.Contains(id))
        {
            throw new ArgumentException("id не корректный");
        }

        if (name == "")
        {
            throw new ArgumentException("Имя аккаунта не может быть пустым");
        }
        _repository.GetRep()[id].Name = name;
    }
}