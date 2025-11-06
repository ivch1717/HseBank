using HseBank.UI;
using HseBank.Commands.AnalyticsComand;
using HseBank.Commands.BankAccountCommand;
using HseBank.Commands.CategoryCommand;
using HseBank.Commands.ExportCommand;
using HseBank.Commands.OperationCommand;
using HseBank.Commands.ImportCommand;

namespace HseBank.Commands;

public class CommandResolver : ICommandResolver
{
    private readonly IInputOutput _inputOutput;
    private readonly Dictionary<string, object> _commands = new();

    public CommandResolver(
        IInputOutput inputOutput,
        
        DifferenceProfitExpense diff,
        GroupingByCategory groupingByCategory,
        Top5ExpensiveExpense top,

        AddAccount addAccount,
        RemoveAccount removeAccount,
        ChangeAccountName changeAccountName,
        GetAllAccounts getAllAccounts,

        AddCategory addCategory,
        RemoveCategory removeCategory,
        ChangeCategoryName changeCategoryName,
        GetAllCategories getAllCategories,

        AddOperation addOperation,
        RemoveOperation removeOperation,
        ChangeOperationDescription changeOperationDescription,
        GetAllOperations getAllOperations,
        
        ExportCategory exportCategory,
        ExportOperation exportOperation,
        ExportAccount exportAccount,
        
        ImportAccountsFromCsv importAccountsFromCsv, 
        ImportAccountsFromJson importAccountsFromJson,
        ImportAccountsFromYaml importAccountsFromYaml,
        
        ImportCategoriesFromCsv importCategoriesFromCsv,
        ImportCategoriesFromJson importCategoriesFromJson,
        ImportCategoriesFromYaml importCategoriesFromYaml,
        
        ImportOperationsFromCsv importOperationsFromCsv,
        ImportOperationsFromJson importOperationsFromJson,
        ImportOperationsFromYaml importOperationsFromYaml
        )
    {
        _inputOutput = inputOutput;

        _commands[nameof(DifferenceProfitExpense)] = diff;
        _commands[nameof(GroupingByCategory)] = groupingByCategory;
        _commands[nameof(Top5ExpensiveExpense)] = top;

        _commands[nameof(AddAccount)] = addAccount;
        _commands[nameof(RemoveAccount)] = removeAccount;
        _commands[nameof(ChangeAccountName)] = changeAccountName;
        _commands[nameof(GetAllAccounts)] = getAllAccounts;

        _commands[nameof(AddCategory)] = addCategory;
        _commands[nameof(RemoveCategory)] = removeCategory;
        _commands[nameof(ChangeCategoryName)] = changeCategoryName;
        _commands[nameof(GetAllCategories)] = getAllCategories;

        _commands[nameof(AddOperation)] = addOperation;
        _commands[nameof(RemoveOperation)] = removeOperation;
        _commands[nameof(ChangeOperationDescription)] = changeOperationDescription;
        _commands[nameof(GetAllOperations)] = getAllOperations;
        
        _commands[nameof(ExportAccount)] = exportAccount;
        _commands[nameof(ExportCategory)] = exportCategory;
        _commands[nameof(ExportOperation)] = exportOperation;
        
        _commands[nameof(ImportAccountsFromCsv)] = importAccountsFromCsv;
        _commands[nameof(ImportAccountsFromJson)] = importAccountsFromJson;
        _commands[nameof(ImportAccountsFromYaml)] = importAccountsFromYaml;
        
        _commands[nameof(ImportCategoriesFromCsv)] = importCategoriesFromCsv;
        _commands[nameof(ImportCategoriesFromJson)] = importCategoriesFromJson;
        _commands[nameof(ImportCategoriesFromYaml)] = importCategoriesFromYaml;
        
        _commands[nameof(ImportOperationsFromCsv)] = importOperationsFromCsv;
        _commands[nameof(ImportOperationsFromJson)] = importOperationsFromJson;
        _commands[nameof(ImportOperationsFromYaml)] = importOperationsFromYaml;
    }

    public ICommand<TRequest> Resolve<TRequest>(string name, bool timed)
    {
        var command = (ICommand<TRequest>)_commands[name];
        return timed ? new TimedCommand<TRequest>(command, _inputOutput) : command;
    }

    public ICommandWithResult<TRequest> ResolveWithResult<TRequest>(string name, bool timed)
    {
        var command = (ICommandWithResult<TRequest>)_commands[name];
        return timed ? new TimedCommandWithResult<TRequest>(command, _inputOutput) : command;
    }
}
