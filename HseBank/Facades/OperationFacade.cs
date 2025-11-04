using HseBank.Factories;
using HseBank.Repository;
using HseBank.service;
namespace HseBank.Facades;

public class OperationFacade : IOperationFacade
{
    ICategoryRepository  _categoryRepository;
    IBankAccountRepository   _accountRepository;
    private IOperationRepository _operationRepository;
    IOperationFactory _factory;
    
    public void AddOperation(int bankAccountId, int amount, DateTime date, int categoryId, string description = "")
    {
        int id = _operationRepository.GetRep().Keys.Max() + 1;
        if (_accountRepository.GetRep().Keys.Contains(bankAccountId))
        {
            throw new ArgumentException("id аккаунта банка не корректный");
        }
        
        if (_categoryRepository.GetRep().Keys.Contains(categoryId))
        {
            throw new ArgumentException("id категории не корректный");
        }
        _operationRepository.Add(id, _factory.Create(id, 
            _categoryRepository.GetRep()[categoryId].Type, bankAccountId, amount, date, categoryId, description));
    }

    public void RemoveOperation(int id)
    {
        if (!_operationRepository.GetRep().Keys.Contains(id))
        {
            throw new ArgumentException("id не корректный");
        }
        _operationRepository.Remove(id);
    }

    public void ChangeDescription(int id, string description)
    {
        if (!_operationRepository.GetRep().Keys.Contains(id))
        {
            throw new ArgumentException("id не корректный");
        }
        _operationRepository.GetRep()[id].Description = description;
    }
}