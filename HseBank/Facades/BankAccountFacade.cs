using HseBank.Export;
using HseBank.Factories;
using HseBank.Repository;
using HseBank.service;

namespace HseBank.Facades;

public class BankAccountFacade : IBankAccountFacade
{
    IBankAccountRepository  _repository;
    IBankAccountFactory _factory;
    IOperationRepository _operationRepository;
    IExportResolver _exportResolver;

    public BankAccountFacade(IBankAccountRepository repository,  IBankAccountFactory factory,  
        IOperationRepository operationRepository, IExportResolver exportResolver)
    {
        _repository = repository;
        _factory = factory;
        _operationRepository = operationRepository;
        _exportResolver = exportResolver;
    }
    public void AddAccount(string name, int balance)
    {
        int id = _repository.IsEmpty() ? 0 :  _repository.GetRep().Keys.Max() + 1;
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

    public void ExportAccount(string filename)
    {
        if (!filename.Contains("."))
        {
            throw new ArgumentException("не праивльное название файла");
        }

        string expansion = filename.Substring(filename.LastIndexOf('.') + 1);
        IExportVisitor visitor = _exportResolver.GetVisitor(expansion);
        foreach (var acc in _repository.GetRep().Values)
        {
            acc.Accept(visitor);
        }

        string res = visitor.GetResult();
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory)
                                 .Parent?.Parent?.Parent?.FullName 
                             ?? AppContext.BaseDirectory;
        
        string dataFolder = Path.Combine(projectRoot, "Data");
        string filePath = Path.Combine(dataFolder, filename);
        File.WriteAllText(filePath, res);
    }

    public string GetAll()
    {
        if (_repository.IsEmpty())
        {
            return "Банковских аккаунтов пока нет";
        }
        string result = "";
        foreach (var acc in _repository.GetRep().Values)
        {
            result += acc.ToString() + '\n';
        }
        return result;
    }
}