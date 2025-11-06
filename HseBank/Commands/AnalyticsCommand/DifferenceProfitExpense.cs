using HseBank.Facades;

namespace HseBank.Commands.AnalyticsComand;

public class DifferenceProfitExpense : ICommandWithResult<PeriodRequest>
{
    IAnalyticsFacade  _analyticsFacade;

    public DifferenceProfitExpense(IAnalyticsFacade analyticsFacade)
    {
        _analyticsFacade = analyticsFacade;
    }
    public string Execute(PeriodRequest request)
    {
        return _analyticsFacade.DifferenceProfitExpense(request.Start, request.End, request.Id).ToString();
    }
}