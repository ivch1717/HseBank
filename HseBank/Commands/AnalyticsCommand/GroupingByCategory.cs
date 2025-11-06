using HseBank.Facades;

namespace HseBank.Commands.AnalyticsComand;

public class GroupingByCategory : ICommandWithResult<int>
{
    IAnalyticsFacade _analyticsFacade;

    public GroupingByCategory(IAnalyticsFacade analyticsFacade)
    {
        _analyticsFacade = analyticsFacade;
    }
    public string Execute(int id)
    {
        return _analyticsFacade.GroupingByCategory(id);
    }
}