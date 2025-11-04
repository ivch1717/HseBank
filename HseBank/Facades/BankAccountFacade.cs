using HseBank.Factories;
using HseBank.Repository;
using HseBank.service;

namespace HseBank.Facades;

public class BankAccountFacade : IBankAccountFacade
{
    IBankAccountRepository  _repository;
    IBankAccountFactory _factory;
    IOperationRepository _operationRepository;

    public BankAccountFacade(IBankAccountRepository repository,  IBankAccountFactory factory,  IOperationRepository operationRepository)
    {
        _repository = repository;
        _factory = factory;
        _operationRepository = operationRepository;
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

        foreach (var operation in _operationRepository.GetRep())
        {
            if (operation.Value.BankAccountId == id)
            {
                _operationRepository.Remove(operation.Key);
            }
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