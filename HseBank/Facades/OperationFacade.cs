using HseBank.Factories;
using HseBank.Repository;
using HseBank.service;
using HseBank.BaseClasses;
using HseBank.TypeOperation;
using HseBank.Export;

namespace HseBank.Facades;

public class OperationFacade : IOperationFacade
{
    private ICategoryRepository  _categoryRepository;
    private IBankAccountRepository   _accountRepository;
    private IOperationRepository _operationRepository;
    private IOperationFactory _factory;
    private IFactoryTypeResolver  _factoryTypeResolver;
    private IExportResolver _exportResolver;

    public OperationFacade(ICategoryRepository categoryRepository, IBankAccountRepository accountRepository,
        IOperationRepository operationRepository, IOperationFactory factory, IFactoryTypeResolver factoryTypeResolver, IExportResolver exportResolver)
    {
        _categoryRepository = categoryRepository;
        _accountRepository = accountRepository;
        _operationRepository = operationRepository;
        _factory = factory;
        _factoryTypeResolver = factoryTypeResolver;
        _exportResolver = exportResolver;
    }
    
    public void AddOperation(int bankAccountId, int amount, int categoryId, string description = "")
    {
        int id = _operationRepository.IsEmpty() ? 0 : _operationRepository.GetRep().Keys.Max() + 1;
        if (!_accountRepository.GetRep().Keys.Contains(bankAccountId))
        {
            throw new ArgumentException("id аккаунта банка не корректный");
        }
        
        if (!_categoryRepository.GetRep().Keys.Contains(categoryId))
        {
            throw new ArgumentException("id категории не корректный");
        }
        DateTime date = DateTime.Now;
        IObserver observer = _accountRepository.GetRep()[bankAccountId];
        observer.Update(_categoryRepository.GetRep()[categoryId].Type, amount);
        _operationRepository.Add(id, _factory.Create(id, 
            _categoryRepository.GetRep()[categoryId].Type, bankAccountId, amount, date, categoryId, observer, description));
    }

    public void RemoveOperation(int id)
    {
        if (!_operationRepository.GetRep().Keys.Contains(id))
        {
            throw new ArgumentException("id не корректный");
        }
        _operationRepository.GetRep()[id].Type = _factoryTypeResolver.GetFactory(_operationRepository.GetRep()[id].Type.RussianName, true).Create();
        _operationRepository.GetRep()[id].Notify();
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

    public void ExportOperation(string filename)
    {
        if (!filename.Contains("."))
        {
            throw new ArgumentException("не праивльное название файла");
        }

        string expansion = filename.Substring(filename.LastIndexOf('.') + 1);
        IExportVisitor visitor = _exportResolver.GetVisitor(expansion);
        foreach (var op in _operationRepository.GetRep().Values)
        {
            op.Accept(visitor);
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
        if (_operationRepository.IsEmpty())
        {
            return "Операций пока нет";
        }
        string result = "";
        foreach (var acc in _operationRepository.GetRep().Values)
        {
            result += acc.ToString() + '\n';
        }
        return result;
    }
}