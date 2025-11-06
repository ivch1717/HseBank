using HseBank.Commands.AnalyticsComand;
using HseBank.Commands.BankAccountCommand;
using HseBank.Commands.CategoryCommand;
using HseBank.Commands.OperationCommand;

namespace HseBank.Commands;

public class RequestResolver : IRequestResolver
{
    private readonly Dictionary<string, Type> _requests = new()
    {
        { nameof(PeriodRequest), typeof(PeriodRequest) },
        
        { nameof(AccountRequest), typeof(AccountRequest) },
        { nameof(AccountRenameRequest), typeof(AccountRenameRequest) },
        
        { nameof(CategoryRequest), typeof(CategoryRequest) },
        { nameof(CategoryRenameRequest), typeof(CategoryRenameRequest) },
        
        { nameof(OperationRequest), typeof(OperationRequest) },
        { nameof(OperationChangeDescriptionRequest), typeof(OperationChangeDescriptionRequest) },
        
        { nameof(Int32), typeof(int) },
        { nameof(Object), typeof(object) }
    };

    public object Resolve(string name)
    {
        var type = _requests[name];
        return Activator.CreateInstance(type)!;
    }
}