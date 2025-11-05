using HseBank.Commands.AnalyticsComand;
using HseBank.UI;

namespace HseBank.Commands;

public class CommandResolver : ICommandResolver
{
    private IInputOutput _inputOutput;
    private Dictionary<string, object> _commands = new();

    public CommandResolver(IInputOutput inputOutput, DifferenceProfitExpense diff, GroupingByCategory groupingByCategory,
        Top5ExpensiveExpense top)
    {
        _inputOutput = inputOutput;
        _commands[nameof(DifferenceProfitExpense)] = diff;
        _commands[nameof(GroupingByCategory)] = groupingByCategory;
        _commands[nameof(Top5ExpensiveExpense)] = top;
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