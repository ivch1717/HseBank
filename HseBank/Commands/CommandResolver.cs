using HseBank.UI;
using HseBank.Commands.AnalyticsComand;
using HseBank.Commands.BankAccountCommand;
using HseBank.Commands.CategoryCommand;
using HseBank.Commands.OperationCommand;

namespace HseBank.Commands;

public class CommandResolver : ICommandResolver
{
    private readonly IInputOutput _inputOutput;
    private readonly Dictionary<string, object> _commands = new();

    public CommandResolver(
        IInputOutput inputOutput,

        // === Аналитика ===
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
        GetAllOperations getAllOperations
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
