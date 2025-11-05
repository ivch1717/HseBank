using HseBank.Commands.AnalyticsComand;
namespace HseBank.Commands;

public class RequestResolver : IRequestResolver
{
    private readonly Dictionary<string, Type> _requests = new()
    {
        {nameof(PeriodRequest), typeof(PeriodRequest)},
        {nameof(Int32),  typeof(int)},
    };
    
    public object Resolve(string name)
    {
        var type = _requests[name];
        return Activator.CreateInstance(type)!;
    }
}