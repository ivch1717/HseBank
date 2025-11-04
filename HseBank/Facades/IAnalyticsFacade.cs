namespace HseBank.Facades;

public interface IAnalyticsFacade
{
    public int DifferenceProfitExpense(DateTime startDate, DateTime endDate, int id);
    public string GroupingByCategory(int id);
    public string Top5ExpensiveExpense(DateTime startDate, DateTime endDate, int id);
}