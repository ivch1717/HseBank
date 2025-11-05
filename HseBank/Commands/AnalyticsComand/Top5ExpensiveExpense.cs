using HseBank.Facades;

namespace HseBank.Commands.AnalyticsComand;

public class Top5ExpensiveExpense : ICommandWithResult<PeriodRequest>
{
    IAnalyticsFacade _analyticsFacade;

    public Top5ExpensiveExpense(IAnalyticsFacade analyticsFacade)
    {
        _analyticsFacade = analyticsFacade;
    }

    public string Execute(PeriodRequest request)
    {
        return _analyticsFacade.Top5ExpensiveExpense(request.Start, request.End, request.Id);
    }
}